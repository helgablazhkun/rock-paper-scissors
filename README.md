# rock-paper-scissors

## Getting started
In order to run the game in your host system you will need to have [Node.js](https://nodejs.org/en) and [.Net Core SDK](https://dotnet.microsoft.com/en-us/download) installed in the system.


## Running the app locally
* Open project folder in the terminal
* Run `dotnet run`
* Open project folder in the second tab of terminal
* Run `npm i`
* Run `ng serve`

## How to play

* Open first browser (Client 1)
* Open second different browser or the same browser in the incognito mode (Client2)
* Visit the app at [http://localhost:4200](http://localhost:4200) for both clients. You should see the text for both Clients: 'There are no active games. Let's initiate it!', input box and 'Start' button.
* Enter some nick for the Client1. You should see the text for the Client 1: "Waiting for another player ...".
* For the Client 2 text should be updated to: 'Join the game and let's play!'. Enter second player nick and press "Join".
* Game board with players score and round info should be displayed.
* Select an option (rock, scissors or paper) for the Client 1. After that you will see your selected choice and "Wait..." status.
* Make a choice for the Client 2. One of the following results for the current round should be shown for both clients: Win, Lose, Draw.
* Continue make choice for both clients.
* The game continues until the compete victory of one of the Clients (3 wins) or draw.
* When game is finish table with game statistic is shown.

## Demo (Video) 
  Demo is [here](https://youtu.be/cEYBq2QAdnQ) 
