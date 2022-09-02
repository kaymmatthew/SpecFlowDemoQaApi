Feature: AddListOfBooks

Background:
	Given I have a book service

@tag1
Scenario: Verify user is able to add list of books with userId and isbn
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a post request to add list of books with userId and isbn to users account
	Then The response code is Created
	And Returned response has the list of books


