
const { keyStores } = nearApi;
const CONTRACT_NAME ="nft-example-1.monukdev2.testnet"
const myKeyStore = new keyStores.BrowserLocalStorageKeyStore();


export function getConfig(network)
{
    switch(network){
        case "testnet":
            return{
                networkId: "testnet",
                keyStore: myKeyStore, // first create a key store
                ContractName: CONTRACT_NAME,
                nodeUrl: "https://rpc.testnet.near.org",
                walletUrl: "https://testnet.mynearwallet.com/",
                helperUrl: "https://helper.testnet.near.org",
                explorerUrl: "https://testnet.nearblocks.io",
            }

            case "mainnet":
                return{
                    networkId: "mainnet",
                    keyStore: myKeyStore, // first create a key store
                    ContractName: CONTRACT_NAME,
                    nodeUrl: "https://rpc.mainnet.near.org",
                    walletUrl: "https://wallet.mainnet.near.org",
                    helperUrl: "https://helper.mainnet.near.org",
                    explorerUrl: "https://nearblocks.io",
                }

                default:
                    throw Error ('Unconfigured environment ',{network});
       

    }

    
}
