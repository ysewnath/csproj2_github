using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Linq;
using TMPro;

public class Account
{
    public string id { get; set; }
    public string access_token { get; set; }
    public string permissions { get; set; }
}


public class LoginManager : MonoBehaviour
{
    [HideInInspector]
    public InputField usernameField, passwordField;
    [HideInInspector]
    public InputField usernameRegisterField, passwordRegisterField, passwordConfirmField;
    [HideInInspector]
    public Text registrationErrorText, registrationCompleteText, submissionText, submissionErrorText;

    public GameObject UserInput;
    public GameObject PassInput;
    public GameObject enterBtn;
    public GameObject closeBtn;

    public GameObject messageBox;
    public GameObject loadingText;
    public GameObject failedConnectText;
    public GameObject connectedText;
    public GameObject deckText;

    public GameObject startBtn;
    public GameObject optionsBtn;
    public GameObject exitBtn;

    [HideInInspector]
    public Button startBtnHandler;
    [HideInInspector]
    public Button optionsBtnHandler;
    [HideInInspector]
    public Button exitBtnHandler;

    public GameObject LoginPrompt;
    public GameObject StartScreenPromt;
    public GameObject DeckSelection;
    public GameObject OptionsPrompt;

    public GameObject options_controls;
    public GameObject options_about;
    public GameObject options_sound;

    [HideInInspector]
    public Button enterBtnHandler;
    [HideInInspector]
    public Button closeBtnHandler;

    [HideInInspector]
    public TMP_InputField userInputHander;
    [HideInInspector]
    public TMP_InputField passInputHandler;

    public GameObject loggedInIcon;
    public GameObject LoggedInAsText;

    public GameObject UserNameText;
    [HideInInspector]
    public TextMeshProUGUI userNameTextHandler;

    public GameObject dropdown;
    [HideInInspector]
    public Dropdown dropdownHandler;

    public LevelChanger levelchanger;


    [SerializeField]
    private SessionManager session;

    private static string baseURL = "https://endlesslearner.com/";
    private static string createAccountURL = baseURL + "register";
    private static string loginAccountURL = baseURL + "login";

    private bool usernameTaken;
    private bool passwordSaved;

    public bool loggedIn = false;
    public bool BypassedLogin = false;
    // Use this for initialization
    void Start()
    {
        //usernameTaken = false;
        //session = new SessionManager();

        //if (PlayerPrefs.GetString("Username").Length > 0)
        //{
        //    usernameField.text = PlayerPrefs.GetString("Username");
        //    passwordField.text = PlayerPrefs.GetString("Password");
        //}

        ////TODO Actually test connection here!
        //if (session.access_token.Length > 0)
        //{
        //    SceneManager.LoadScene("MainMenu");
        //}

        userInputHander = UserInput.GetComponent<TMP_InputField>();
        passInputHandler = PassInput.GetComponent<TMP_InputField>();
        passInputHandler.inputType = TMP_InputField.InputType.Password;
        enterBtnHandler = enterBtn.GetComponent<Button>();
        closeBtnHandler = closeBtn.GetComponent<Button>();

        startBtnHandler = startBtn.GetComponent<Button>();
        optionsBtnHandler = optionsBtn.GetComponent<Button>();
        exitBtnHandler = exitBtn.GetComponent<Button>();

        userNameTextHandler = UserNameText.GetComponent<TextMeshProUGUI>();

        dropdownHandler = dropdown.GetComponent<Dropdown>();



    }

    public void OnLoginClick()
    {
        string username = userInputHander.text;
        //PlayerPrefs.SetString("Password", passwordField.text);
        string password = passInputHandler.text;

        messageBox.SetActive(true);
        loadingText.SetActive(true);
        failedConnectText.SetActive(false);
        connectedText.SetActive(false);
        enterBtnHandler.interactable = false;
        closeBtnHandler.interactable = false;

        userInputHander.interactable = false;
        passInputHandler.interactable = false;

        StartCoroutine(LoginAccount(username, password));

    }

    public void TutorialStar()
    {
        //
        // check if deck has enough cards for the level
        //



        int menuIndex = dropdownHandler.value;
        List<Dropdown.OptionData> menuOptions = dropdownHandler.options;
        string selectedDeckTemp = menuOptions[menuIndex].text;

        foreach (DeckInfo deck in session.decks)
        {
            if (selectedDeckTemp == deck.name)
            {
                Debug.Log("found selected deck from dropdown and placed it into scriptable object");
                session.selectedDeck = deck;
                break;
            }
        }

        if (session.selectedDeck == null)
        {
            Debug.Log("could not find selected deck");
            return;
        }

        //
        // screen wipe transition and load level;
        //
        levelchanger.FadeToLevel(2);

    }

    public void OnRegisterClick()
    {
        usernameField.text = "";
        passwordField.text = "";
        submissionErrorText.text = "";
        submissionText.text = "";
    }

    public void OnSubmitClick()
    {
        // Check if username is too long or short
        if (usernameRegisterField.text.Length < 2 || usernameRegisterField.text.Length > 8)
        {
            registrationErrorText.text = "Username must be between 2-8 characters long";
        }
        // Ensures the username is only letters and numbers
        else if (!Regex.IsMatch(usernameRegisterField.text, @"^[a-zA-Z0-9]+$"))
        {
            registrationErrorText.text = "Username can only contain letters and numbers";
        }
        else if (passwordRegisterField.text.Length < 4 || passwordRegisterField.text.Length > 20)
        {
            registrationErrorText.text = "Password must be between 4-20 characters long";
        }
        // Check if passwords match
        else if (passwordRegisterField.text != passwordConfirmField.text)
        {
            registrationErrorText.text = "Passwords do not match";
        }
        // Account registration information is valid
        else
        {
            string password = passwordRegisterField.text;

            StartCoroutine(RegisterAccount(usernameRegisterField.text, password));
        }
    }

    public void OnBackClick()
    {
        usernameRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordConfirmField.text = "";
        registrationErrorText.text = "";
        registrationCompleteText.text = "";
        usernameRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordConfirmField.text = "";
        submissionErrorText.text = "";
        submissionText.text = "";
    }

    IEnumerator RegisterAccount(string username, string passwordHash)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            // Fields must be the same as they are in the Python script on the server
            new MultipartFormDataSection("username", "ii"),
            new MultipartFormDataSection("password", "ii"),
        };

        UnityWebRequest www = UnityWebRequest.Post(createAccountURL, formData);
        yield return www;

        if (www.downloadHandler.text.ToString().Contains("User already exists"))
        {
            registrationErrorText.text = "Username already exists";
            registrationCompleteText.text = "";
            usernameTaken = true;
        }
        else
        {
            registrationErrorText.text = "";
            registrationCompleteText.text = "Registration Complete!";
        }

        // Resets the password fields
        passwordRegisterField.text = "";
        passwordConfirmField.text = "";
    }

    IEnumerator LoginAccount(string username, string password)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            // Fields must be the same as they are in the Python script on the server
            new MultipartFormDataSection("username", username),
            new MultipartFormDataSection("password", password),
        };

        UnityWebRequest www = UnityWebRequest.Post(loginAccountURL, formData);
        yield return www.SendWebRequest();

        string dat = www.downloadHandler.text;

        if (!dat.Contains("Invalid credentials!") && !dat.Equals(""))
        {
            connectedText.SetActive(true);
            loadingText.SetActive(false);

            Account user = JsonConvert.DeserializeObject<Account>(dat);
            //submissionText.text = user.id + " - logging in...";
            //submissionErrorText.text = "";
            session.access_token = user.access_token;
            session.id = user.id;
            Debug.Log(user.access_token);

            yield return new WaitForSeconds(1);
            yield return StartCoroutine(GetDeckNames());
            //EditorUtility.SetDirty(session);
            //SceneManager.LoadScene("MainMenu");
        }
        else
        {
            //submissionErrorText.text = dat;
            //submissionText.text = "";
            loadingText.SetActive(false);
            failedConnectText.SetActive(true);
            enterBtnHandler.interactable = true;
            closeBtnHandler.interactable = true;

            userInputHander.interactable = true;
            passInputHandler.interactable = true;

        }
    }

    public void FillOptionsList()
    {
        foreach (DeckInfo deck in session.decks)
        {
            dropdownHandler.options.Add(new Dropdown.OptionData() { text = deck.name });
        }
    }

    IEnumerator GetDeckNames()
    {
        connectedText.SetActive(false);
        deckText.SetActive(true);
        Debug.Log("get deck names");
        UnityWebRequest www = UnityWebRequest.Get(baseURL + "decks");
        www.SetRequestHeader("Authorization", "Bearer " + session.access_token);
        yield return www.SendWebRequest();

        string decks = www.downloadHandler.text;

        if (!decks.Contains("Invalid credentials!"))
        {
            DecksJson deckLists = JsonConvert.DeserializeObject<DecksJson>(decks);
            session.decks = deckLists.ids.Zip(deckLists.names, (a, b) => new DeckInfo(a, b)).ToList();
            //Debug.Log(decks);
            //EditorUtility.SetDirty(session);
            yield return StartCoroutine(session.DownloadDecks());
        }
        else
        {
            Debug.Log("error getting decks");
        }
    }
}