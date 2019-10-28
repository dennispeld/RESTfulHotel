# RESTful Hotel
The *RESTfulHotel* is a Web API project, that implements a web service with GET request, which imports a json file with hotel rates, filters the list by means of the parameters and returns a filtered list.

### Input
File hotelsrates.json based in the root of the project

### Output
Output is a HTTP response with a JSON string body

### Implementation
To implement this task a new Web API template for .NET Core 2.2 was used.  

**Models**  
*Hotels.cs* - Json input file was converted into a model with classes, where the RootObject is the root of json file, as well as Hotel, HotelRate, Price and RateTag classes.

**Controllers**  
*HotelsController.cs* - All the API routes and the business logic is specified there. One can
1. Retrieve all hotels
2. Search by hotel id
3. Search by arrival date
4. Search by hotel id AND arrival date

When you call each one of those 4 methods, at first the method InitHotels() is called, which only function is to read the hotelrates.json file and deserialize it. Then we are having a list of RootObjects, which we use to search by criteria using LINQ.  
One of the major problems was the search by arrival date, because of the Date format. For this, I wrote an extra method FormatArrivalDate(string arrDateString), that formats it the way it is used in json file.
		
### Testing
For testing I have used a Postman program installed locally.  

Three different scenarios were tested:
1. Search by HotelID  
HotelID = 8759  
ArrivalDate = empty

2. Search by ArrivalDate  
HotelID = empty  
ArrivalDate = 16.03.2016 (also works with such formats 16/03/2016 and 2016-03-16)
	
3. Search by HotelID and ArrivalDate  
HotelID = 7294  
ArrivalDate = 15.03.2016

### Usage and test cases
1. Clone the project
2. Run the project (using RESTfulHotel launch settings profile)
3. Open Postman program [www.getpostman.com] and type the API URL that you want to test
4. Select GET method  
5. Type https://localhost:5001/api/hotels and hit Send button to retrieve all hotels and ratings
6. Type https://localhost:5001/api/hotels/7294 and hit Send button to retrieve a hotel with id 7294 and its ratings
7. Type https://localhost:5001/api/hotels/arrival/16.03.2016 and hit Send button to retrieve hotels that have ratings with the arrival date of 16.03.2016
8. Type https://localhost:5001/api/hotels/7294/arrival/15.03.2016 and hit Send button to retrieve a hotel with the id 7294 and ratings with the arrival date of 16.03.2016

Author: Dennis Peld  
Language: C#, .NET Core 2.2  
Environment: JetBrains Rider
