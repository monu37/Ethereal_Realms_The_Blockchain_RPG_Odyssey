using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RFG.NEAR;
using System.Threading.Tasks;

public class NearController : MonoBehaviour
{
  [SerializeField] private string _contractId = "nft-example-1.monukdev2.testnet";
  [SerializeField] private string _network = "testnet";

  [SerializeField] private TextMeshProUGUI _textWelcome;
  [SerializeField] private TextMeshProUGUI _textTokens;
  [SerializeField] Button _buttonSignInOut;
  [SerializeField] UIToken _uiTokenPrefab;
  [SerializeField] Transform _uiTokenParent;

  private string _accountId;
  private bool _signedIn;

  void OnEnable()
  {
    NearCallbacks.OnSignIn += OnSignIn;
    NearCallbacks.OnGetAccountId += OnGetAccountId;
    NearCallbacks.OnNftTokensForOwner += OnNftTokensForOwner;
  }

  void OnDisable()
  {
    NearCallbacks.OnSignIn -= OnSignIn;
    NearCallbacks.OnGetAccountId -= OnGetAccountId;
    NearCallbacks.OnNftTokensForOwner -= OnNftTokensForOwner;
  }

  void Start()
  {
    NearAPI.IsSignedIn(_network);
  }

  private void OnSignIn(bool signedIn)
  {

    _signedIn = signedIn;
    if (_signedIn)
    {
      NearAPI.GetAccountId(_network);
      _buttonSignInOut.GetComponentInChildren<TMP_Text>().text = "Sign Out";
      _textTokens.text = "Click Players to choose player...";
    }
    else
    {
      _textWelcome.text = "You must sign in...";
      _textTokens.text = "You must sign in...";
      _buttonSignInOut.GetComponentInChildren<TMP_Text>().text = "Sign In";
    }
  }
  private void OnGetAccountId(string accountId)
  {
    _accountId = accountId;
        //_textWelcome.text = "Welcome " + _accountId;
        _textWelcome.text = "Welcome " + accountId;
  }
  private async void OnNftTokensForOwner(Token[] tokens)
  {
    foreach (Transform child in _uiTokenParent)
    {
      Destroy(child.gameObject);
    }

    foreach (var token in tokens)
    {
      _textTokens.text = "Select a Player to continue...";

      // Get the image
      Texture texture = await RestAPI.GetImage(token.metadata.media);
      if (texture != null)
      {
        UIToken uiToken = Instantiate(_uiTokenPrefab);
        uiToken.SetImage(texture);
        uiToken.SetTitle(token.metadata.title);
        uiToken.SetTokenId(token.token_id);
        uiToken.SetButtonListener(OnTokenButtonClicked);
        uiToken.transform.SetParent(_uiTokenParent);
        uiToken.transform.localScale = Vector3.one;
        uiToken.transform.localPosition = Vector3.zero;
        _uiTokenParent.localPosition = Vector3.zero;
      }
    }

  }

    public void selectplayer()
    {
        UIMainMenu.instance.openmainpanel();
    }

  void OnTokenButtonClicked(string tokenId)
  {
    Debug.Log("OnTokenButtonClicked: " + tokenId);
    _textTokens.text = "Player : " + tokenId + " is selected!";

        Invoke(nameof(selectplayer), .2f);
       
  }



  public void NftTokensForOwner()
  {
    if (!_signedIn)
    {
      _textTokens.text = "You must sign in to get player...";
      return;
    }
    NearAPI.NftTokensForOwner(_accountId, _contractId, _network);
  }
  public void SignInOut()
  {

    if (_signedIn)
    {
      NearAPI.SignOut(_network);
      NearAPI.IsSignedIn(_network);
    }
    else
    {
      NearAPI.RequestSignIn(_textWelcome,_contractId, _network);//This will do a page refresh anyway
      NearAPI.IsSignedIn(_network);
    }


  }

}
