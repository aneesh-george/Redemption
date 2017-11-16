// Client ID and API key from the Developer Console
var CLIENT_ID;
var API_KEY;
// Array of API discovery doc URLs for APIs used by the quickstart
var DISCOVERY_DOCS;

// Authorization scopes required by the API; multiple scopes can be
// included, separated by spaces.
var SCOPES;
var authorizeButton;
var signoutButton;
var listCalenderButton;
var setCalenderButton;

/**
 *  On load, called to load the auth2 library and API client library.
 */
function handleClientLoad() {
    // Client ID and API key from the Developer Console
    CLIENT_ID = '768348725301-um8q8asnqpocrqueka841t9lhqsi588r.apps.googleusercontent.com';
    API_KEY = 'AIzaSyCOGVs2UU794rbuBRaLf3Q2AyHa__SwA1w';

    // Array of API discovery doc URLs for APIs used by the quickstart
    DISCOVERY_DOCS = ["https://www.googleapis.com/discovery/v1/apis/calendar/v3/rest"];

    // Authorization scopes required by the API; multiple scopes can be
    // included, separated by spaces.
    SCOPES = "https://www.googleapis.com/auth/calendar.readonly";

    authorizeButton = document.getElementById('authorize_button');
    signoutButton = document.getElementById('signout_button');
    listCalenderButton = document.getElementById('view_cal_button');
    //setCalenderButton = document.getElementById('set_cal_button');

    gapi.load('client:auth2', initClient);
}

/**
 *  Initializes the API client library and sets up sign-in state
 *  listeners.
 */
function initClient() {
    gapi.client.init({
        apiKey: API_KEY,
        clientId: CLIENT_ID,
        discoveryDocs: DISCOVERY_DOCS,
        scope: SCOPES
    }).then(function () {
        // Listen for sign-in state changes.
        gapi.auth2.getAuthInstance().isSignedIn.listen(updateSigninStatus);

        // Handle the initial sign-in state.
        updateSigninStatus(gapi.auth2.getAuthInstance().isSignedIn.get());
        handleAuthClick();
        authorizeButton.onclick = handleAuthClick;
        signoutButton.onclick = handleSignoutClick;
    });
}

/**
 *  Called when the signed in status changes, to update the UI
 *  appropriately. After a sign-in, the API is called.
 */
function updateSigninStatus(isSignedIn) {
    debugger;
    if (isSignedIn) {
        authorizeButton.style.display = 'none';
        signoutButton.style.display = 'block';
        listCalenderButton.style.display = 'block';
        //setCalenderButton.style.display = 'block';
        document.getElementById('patientDetails').style.display = 'block';
        
    } else {
        authorizeButton.style.display = 'block';
        signoutButton.style.display = 'none';
        listCalenderButton.style.display = 'none';
        //setCalenderButton.style.display = 'none';
        document.getElementById('patientDetails').style.display = 'none';
    }
}

/**
 *  Sign in the user upon button click.
 */
function handleAuthClick(event) {
    gapi.auth2.getAuthInstance().signIn();
    
}



/**
 *  Sign out the user upon button click.
 */
function handleSignoutClick(event) {
    gapi.auth2.getAuthInstance().signOut();
}

/**
 * Append a pre element to the body containing the given message
 * as its text node. Used to display the results of the API call.
 *
 * @param {string} message Text to be placed in pre element.
 */
function appendPre(message) {
    var pre = document.getElementById('content');
    var textContent = document.createTextNode(message + '\n');
    pre.appendChild(textContent);
}

/**
 * Print the summary and start datetime/date of the next ten events in
 * the authorized user's calendar. If no events are found an
 * appropriate message is printed.
 */
function listUpcomingEvents() {
    gapi.client.calendar.events.list({
        'calendarId': 'primary',
        'timeMin': (new Date()).toISOString(),
        'showDeleted': false,
        'singleEvents': true,
        'maxResults': 10,
        'orderBy': 'startTime',
        'etag': 'rh',
        'q': 'Redemption'
    }).then(function(response) {
        var events = response.result.items;
        appendPre('Upcoming events:');

        if (events.length > 0) {
            for (i = 0; i < events.length; i++) {
                var event = events[i];
                var when = event.start.dateTime;
                if (!when) {
                    when = event.start.date;
                }
                appendPre(event.summary + ' (' + when + ')')
            }
        } else {
            appendPre('No upcoming events found.');
        }
    });
}
	  
	  
function setAppointment(pName, pAge, pDept, pDoc, pDate){
    var event = {
        'summary': 'Consultation with ' + pDoc,
        'location': 'Redemption Hospital: Department of '+ pDept,
        'description': 'Consultation with ' + pDoc + ' at Redemption Hospital: Department of '+ pDept ,
        'start': {
            'date': pDate, //'2017-12-18T15:00:00-18:00',
            'timeZone': 'Asia/Kolkata'//'America/Los_Angeles'

        },
        'end': {
            'date': pDate,
            'timeZone': 'Asia/Kolkata'
        },
        'etag': 'rh',
        
        //'recurrence': [
        //  'RRULE:FREQ=DAILY;COUNT=2'
        //],
        //'attendees': [
        //  {'email': 'lpage@example.com'},
        //  {'email': 'sbrin@example.com'}
        //],
        //'reminders': {
        //    'useDefault': false,
        //    'overrides': [
        //      {'method': 'email', 'minutes': 24 * 60},
        //      {'method': 'popup', 'minutes': 10}
        //    ]
        //}
    };

    var request = gapi.client.calendar.events.insert({
        'calendarId': 'primary',
        'resource': event
    });

    request.execute(function(event) {
        appendPre('Event created: ' + event.htmlLink);
    });
};
