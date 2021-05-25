# InfoTrack WebSearch 
InfoTrack coding assignment.

I have implemented a simple website using ASP.NET MVC v5.0.

## Building and testing

Clone the repo, then build and debug using Visual Studio 2019.

Note that Google returns a few extra results above the number requested, e.g. some ads at the top, so the from position can sometimes be greater than the number of results requested.

The search history is an in-memory list, so only lasts for the duration of the session.

The Search and Search History services are injected a startup, allowing different implementations in future, e.g. for the Search History to be persisted to a SQL Database, or to use a search enging other than Google. See sample Bing search implementation, although this does not work, possibly due to it needing to click past the privacy dialog.

This also allows unit testing of the separate components: I have used xUnit and Moq to implement 2 very simple unit tests, but time does not allow me to do any more, this is just to demonstrate the concept. The tests can be run in the VIsual Studio test runner.

## Extra things left to do

Add more Unit tests and Integration tests

Add Logging

Error handling

Show recent search terms in drop-down to save typing

Ability to re-run previous searches from history page

Show spinner while waiting for search to coomplete

Persist searches to SQLServer database

Fix Bing search which is not currently working

Implement more search engines e.g. Yahoo, DuckDuckGo

Parse the returned HTML document instead of using regex, to filter out unwanted links, e.g. ads


