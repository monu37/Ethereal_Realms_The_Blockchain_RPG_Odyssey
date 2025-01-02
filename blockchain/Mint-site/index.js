import { getConfig } from "./config.js";

const nearConfig = getConfig("testnet");
const { connect, WalletConnection, Contract, utils } = nearApi;  // Correct usage with the global `nearApi`

const SignInButton = document.getElementById("Sign-in-button");
const SignOutButton = document.getElementById("Sign-out-button");
const AccountNameId = document.getElementById("Account");
const MintDiv = document.getElementById("mint");
const MintButton = document.getElementById("mint-button");
const TokenIdInput = document.getElementById("token-id");
const TitleIdInput = document.getElementById("title");
const ImageUrlIdInput = document.getElementById("image-url");

const nearConnection = await connect(nearConfig);
    const walletConnection = new WalletConnection(nearConnection);

    SignInButton.onclick = () => walletConnection.requestSignIn(nearConfig.ContractName);
    SignOutButton.onclick = () => {
        walletConnection.signOut();
        window.location.reload();
    };

    MintButton.onclick = async () => {
        console.log("Minting...", TokenIdInput.value, TitleIdInput.value, ImageUrlIdInput.value);
        const accountId = walletConnection.getAccountId();
        const account = walletConnection.account();
        const contract = new Contract(account, nearConfig.ContractName, {
            viewMethods: [],
            changeMethods: ["nft_mint"], 
        });

        try {
            await contract.nft_mint({
                token_id: TokenIdInput.value,
                metadata: {
                    title: TitleIdInput.value,
                    description: "Player from the game",
                    media: ImageUrlIdInput.value,
                },
                receiver_id: accountId,
            },
            300000000000000,  // Adjusted gas
            utils.format.parseNearAmount("1")); // Adjusted deposit
        } catch (error) {
            console.error("Minting error:", error);
        }
    };

    window.onload = async () => {
        let isSignedIn = walletConnection.isSignedIn();
        if (isSignedIn) {
            console.log("Signed in!");
            SignInButton.style.display = "none";
            let accountid = walletConnection.getAccountId();

            AccountNameId.innerText = "Signed in as " + accountid;
            const contract = new Contract(walletConnection.account(), nearConfig.ContractName, {
                viewMethods: ["nft_tokens_for_owner"],
            });
            const tokens = await contract.nft_tokens_for_owner({ account_id: accountid });

            console.log("Tokens ", tokens);

            let tokendiv = document.getElementById("tokens");
            tokens.forEach((token) => {
                let tokenimg = document.createElement("img");
                tokenimg.src = token.metadata.media;
                tokenimg.style.width = "100px";
                tokendiv.appendChild(tokenimg);
            });
        } else {
            console.log("Not signed in!");
            SignOutButton.style.display = "none";
            MintDiv.style.display = "none";
        }
    }



