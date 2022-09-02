Feature: Delete

Background:
	Given I have a book service

@tag1
Scenario: Verify User sends a delete request to delete user with userId
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a delete request to delete user with userId
	Then Status response code is NoContent

@tag1
Scenario: Verify user is able to delete list of books with userId
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a post request to add list of books with userId and isbn to users account
	Then The response code is Created
	And Returned response has the list of books
	When I send a delete request to delete books with userId
	Then Returned response code is NoContent

@tag1
Scenario: Verify user is able to delete specific book with isbn and userId
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a post request to add book with userId and isbn to users account
	Then The response code is Created
	And Returned response has isbnNumber
	When I send a delete request to delete a specific book with isbnNumber and userId
	Then Returned response code is NoContent
	When I send a delete request to delete a specific book with isbnNumber and userId
	#When I send a Get request to retrieve a specific deleted book with isbnNumber
	Then The status code is BadRequest