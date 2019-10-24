# API Test

Test of API are implementend in the postman collection. Each request comes with the test script. What you need to run manually the test suite:

1. Open Postman Runner from file menu
2. Select the collection
3. Start

The first request get the token using bob's credential, this user it's part of the data seed.

Test are processed in sequential order, so POST request have to be done BEFORE GET if you want to save data then check if are OK 

## d