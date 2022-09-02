Feature: Get all books

Background:
	Given I have a book service

@mytag
Scenario: Get all books test
	When I send a Get request to retrieve all books
	Then Response code is OK
	And Response body contains:
		| author               | publisher      | subTitle               |
		| Richard E. Silverman | O'Reilly Media | A Working Introduction |

@mytag
Scenario: Get a specific user with userId
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a Get request to retrieve a specific user with userId
	Then Response statusCode is OK
	And Returned response has correct info

@mytag
Scenario: Verify user is able to send a Get request to get a specific book with isbn
	When I send a request to create new user with username and password
	Then The response code is Created
	And The returned response has the right user info
	When I send a post request to add book with userId and isbn to users account
	Then The response code is Created
	And Returned response has isbnNumber
	When I send a Get request to retrieve a specific book with isbnNumber
	Then The Status response code is OK
	And Returned response has book info