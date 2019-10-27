# RESTful Hotel
The *RESTfulHotel* is a Web API project, that implements a web service with GET request, which imports a json file with hotel rates, filters the list by means of the parameters and returns a filtered list.

### Input
Method -> String, stream, JsonObject or file based on the file hotelsrates.json  
Browser -> HotelID and ArrivalDate

### Output
Output is a HTTP response with a JSON string body

### Implementation
To implement this task a new Web API template for .NET Core 2.2 was used.  

**App_Start**  
*WebApiConfig.cs* - That's where the Web API routes were registered. To separate 3 different scenarios, 
I gave to each route its own unique name, which mimics the name of the method in a controller.

**Models**  
*Hotels.cs* - Json input file was converted into a model with classes, where the RootObject is the root of json file, as well as Hotel, HotelRate, Price and RateTag classes.

**Controllers**  
*HotelsController.cs* - The file, which does the main job. Here are implemented 3 scenarios:
1. Search by HotelID
2. Search by ArrivalDate
3. Search by HotelID AND ArrivalDate

When you call each one of those 3 methods, at first the method InitHotels() is called, which only function is to read the hotelrates.json file and deserialize it. Then we are having a list of RootObjects, which we use to search by criteria using LINQ.  
One of the major problems was the search by ArrivalDate, because of the Date format. For this, I wrote an extra method formatArrivalDate(string arrDateString), that formats it the way it is used in json file.
		
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
2. Run the project
3. Open Postman program [www.getpostman.com] and type the API URL that you want to test
4. Select GET method  
5. Type https://localhost:5001/api/hotels and hit Send button to retrieve all hotels and ratings
6. Type https://localhost:5001/api/hotels/7294 and hit Send button to retrieve a hotel with id 7294 and its ratings
7. Type https://localhost:5001/api/hotels/arrival/16.03.2016 and hit Send button to retrieve hotels that have ratings with the arrival date of 16.03.2016
8. Type https://localhost:5001/api/hotels/7294/arrival/15.03.2016 and hit Send button to retrieve a hotel with the id 7294 and ratings with the arrival date of 16.03.2016

Author: Dennis Peld  
Language: C#, .NET Core 2.2  
Environment: JetBrains Rider