using Firebase.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class ResetPassword : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    public TMP_InputField emailInputField;

    [SerializeField] TMP_Text messageBox;

    private void Awake()
    {
        messageBox.enabled = false;
    }

    public void SendPasswordResetEmailButton()
    {
        SendPasswordResetEmail(emailInputField.text);
        messageBox.enabled = true;
        messageBox.text = "Acesse seu e-mail para recuperar a senha!";
    }

    private void SendPasswordResetEmail(string email)
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }
}
