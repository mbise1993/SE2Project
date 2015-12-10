Acceptance Tests
----

*User Story 1
@Normal_Flow
Scenario: Added User
Given that I am on the Add User screen
	And I am logged in as an administrator
	And I enter "hartsockc" as username
	And I enter "password123" as password
When I press Add User
Then a new user named hartsockc is created
	And they can log into the system using password123.
Status: Success
	
@Exceptions_Flow
Scenario: Inadequate Permissions To Add User
Given that I am on the Add User screen
	And I am logged in as a regular user
	And I enter "hartsockc" as username
	And I enter "password123" as password
When I press Add User
Then an inadequate permissions error is displayed
	And no new user is created.
Status: Success

*User Story 2
@Normal_Flow
Scenario: Find Client
Given I am on the homepage
	And I select the Client filter
	And I select the LastName field
	And I enter "Client0Last"
When I press the search button
Then client Client0First A. Client0Last appears.
Status: Success

@Exceptions_Flow
Scenario: Failed Client Search
Given I am on the homepage
	And I select the Client filter
	And I select the LastName field
	And I enter "Person"
When I press the search button
Then an appropriate error message appears.
Status: Success

*User Story 3
@Normal_Flow
Scenario: Streamlined Workflow
Given that I am on a tab for a certain client
	And I have another tab with another client on it
When I click on the tab for the other client
Then the program switches tabs quickly and seamlessly.
Status: Success

*User Story 4
@Normal_Flow
Scenario: Edit Client
Given that I am on the Edit Client page
	And I change state from TN to VA
When I click Save Client button 
Then the state will be changed in the database.
Status: Success

*User Story 5
@Normal_Flow
Scenario: Simple Login
Given that I am on the login screen
	And I enter "leaderat" as my username
	And I enter "password" as my password
When I click the Login button
Then the application's homepage appears.
Status: Success

@Exceptions_Flow
Scenario: Failed Login
Given that I am on the login screen
	And I enter "user1" as my username
	And I enter "password" as my password
When I click the Login button
Then an incorrect username / password error appears
	And the application remains on the login screen.
Status: Success
	
*User Story 6
@Normal_Flow
Scenario: Add Client
Given that I am on the Add Client screen
	And I have entered all necessary information
When I click Add Client button
Then the client will be added to the database.
Status: Success

*User Story 7
@Normal_Flow
Scenario: View Client Info
Given I have searched for a given client
When I click on the client's name
Then the client's information opens in a tab.
Status: Success

*User Story 8
@Normal_Flow
Scenario: View Chart 
Given I am on the Graphing screen
When I click Active chart 
Then the Active chart opens in a tab.
Status: Unsure

