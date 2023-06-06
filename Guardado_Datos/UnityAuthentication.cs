using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.CloudSave;

public class UnityAuthentication : MonoBehaviour {

    //private async void Start() {
    //    await UnityServices.InitializeAsync();
    //    Debug.Log(UnityServices.State);
    //    SetupEvents();
    //    await SignInAnonymouslyAsync();
    //    await SaveSomeData();
    //}

    //private void SetupEvents() {
    //    AuthenticationService.Instance.SignedIn += () => {
    //        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
    //        Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
    //    };
    //    AuthenticationService.Instance.SignInFailed += (err) => {
    //        Debug.LogError(err);
    //    };
    //    AuthenticationService.Instance.SignedOut += () => {
    //        Debug.Log("Signed out");
    //    };

    //    AuthenticationService.Instance.Expired += () => {
    //        Debug.Log("Expired");
    //    };
    //}

    //private async Task SignInAnonymouslyAsync() {
    //    try {
    //        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    //        Debug.Log("Signed in anonymously");
    //        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
    //    } catch (AuthenticationException ex) {
    //        Debug.LogException(ex);
    //    } catch (RequestFailedException ex) {
    //        Debug.LogException(ex);
    //    }
    //}
    //public async Task SaveSomeData() {
    //    var data = new Dictionary<string, object> { { "key", "someValue" } };
    //    await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    //}

}
