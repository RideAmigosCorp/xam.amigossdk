
# Overview

The RAWebView is built on a XamarinForms WebView. It support most/all existing WebView methods and proprties. It will be customized by RideAmigos to simplify the interactions with the Workplace app by providing additional properties, convenience methods, and callbacks focused on this particular use case.

# Implemented Aspects

## Initialization 
Basic initialization will be performed by instantiating an instance of the RAWebView and passing in a `JWTString` (the encrypted/encoded object containing app user details which include, at a minimum, `Email`,`FirstName`, and `LastName`. Please see this example of a suitable server-side [JWT creation implementation](https://github.com/jwt-dotnet/jwt#creating-encoding-token).

### Example 1 - C#
```
// Initialize the Webview with the JWT
RAWebView WebView = new RAWebView() {
    JWTString = commuterJWTString
}
```

### Example 2 - XAML
```
// Initialize the Webview with the JWT
    <amigos:RAWebView 
      BindingContext="{x:Reference pageModel}"
      x:Name="RideAmigosContent" 
      JWTString="{Binding Path=commuterJWTSTring}" 
      />
```


## Optional `PageContext`

Initializing the view with a specific context will be supported. This will include:
  - `action` - a string targeting a specific section of the Commute experience to load for the user (the possible values will be defined as we proceed with development)
  - `metadata` - an anonymous object (at least temporarily) that will include any relevant details which could be used during the course of the user's interactions with the commute content. One suggested element would be an OriginalPageName which could be used to return the user to the appropriate place in the Workplace App after a successful interaction / workflow completion

### Example

```
String commuterJWTString = "JWT String";

dynamic metadata = new ExpandoObject();
metadata.OriginalPageName = "home";

pageContext = new RAWebViewContext
{
    action = "Main Menu",
    metadata = metadata
};

// Initialize the Webview with the JWT
RAWebView WebView = new RAWebView() {
    JWTString = commuterJWTString,
    // optional page context
    PageContext = pageContext
}
```

## Optional Android Back Button and Close Support

To utilize the Android back button, there are two additional aspects which must be implemented.
 
 - `WebView.BackPressed()` - After the back button has been pressed, the xamarin app should intercept that event and call the `BackPressed` method on the instance of the `RAWebView`. This tells the running RideAmigos application that the button has been pressed and allows it to take the appropriate action (e.g., transitioning state OR triggering the `OnCloseRequested` event)
 - `WebView.OnCloseRequested += OnCloseRequested` - provide an event handler to take action when the sdk requests to be closed

### Example

```
String commuterJWTString = "JWT String";

// Initialize the Webview with the JWT
RAWebView WebView = new RAWebView() {
    JWTString = commuterJWTString,
    // Attach the event handler 
    OnCloseRequested += OnCloseSDK
}

private void OnCloseSDK(object sender, EventArgs e)
{
  // trigger internal navigation or close the SDK window
  DisplayAlert("Close Requested", "The SDK has requested to close.", "OK");
}

protected override bool OnBackButtonPressed()
{
    WebView.BackPressed();
    return true;
}
```

# Planned API Additions

## Status, Exception, and Navigation Callbacks

Pre-defined callbacks will be supported for handling the key interaction requirements between the Workplace App and the RideAmigos WebView content.

While the exact structure of the specific events is to be determined, these three callbacks should be supported.

 - `OnUserStatusChange` - this method will be called to inform the Workplace App of a change to a users' parking reservation or commute mode choice
 - `OnException` - this will be called in the event that an unrecoverable exception has occurred during initialization or user interaction. The specific exception types will be clearly defined at a later date but are likely to include:
   - Authentication Error - there was a problem processing or verifying the JWTString
   - Content Load Error - there was a problem retrieving and loading the content from the RideAmigos servers
   - Workflow Error - for a reason where an unrecoverable error has occurred within the workflow itself and the user needs to close or otherwise reload the content.


### Example
```
WebView.OnUserStatusChange += OnCommuteUserStatusChange;
WebView.OnException += OnException;

private void OnCommuteUserStatusChange(UserStatusChangeDelegate eventObj)
{}
private void OnException(RAWebViewExceptionDelegate eventObj){}
```
