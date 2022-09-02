Feature: CreateNewUser

@tag1
Scenario: Create new user test
	Given I have a book service
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info