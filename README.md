
# URL Shortener

## Introduction

This is a simple URL shortener web application that allows users to create shortened URLs, which can then be used to redirect to the original URL. In addition to the basic functionality of a URL shortener, this application also includes features such as location detection, expiry date for links, user management, and a statistics page with a powerful data table.

Redirect url Example : [http://naserelz010-001-site1.ftempurl.com/r/zjOgKU9](http://naserelz010-001-site1.ftempurl.com/r/zjOgKU9)

## Features

### User Identity 

- Log in 
- Register
- Log out
- Email Confirmation
- Two Factor Authentication
- Remember Me .option
- and many more 

### URL Shortening

-  Allows users to create shortened URLs for any website
-  Shortened URLs can be used to redirect to the original URL
- Powerful Redirect Logic before Redirecting

### Location Detection

-   Detects the location of the user accessing the shortened URL
-   Displays the country of the user on the statistics page
-   Example :
	"{"status":"success","continent":"Asia","continentCode":"AS","country":"Israel","countryCode":"IL","region":"M","regionName":"Central District","city":"Raanana","district":"","zip":"","lat":32.1865,"lon":34.8726,"timezone":"Asia/Jerusalem","offset":10800,"currency":"ILS","isp":"XFone 018 Ltd","org":"XFone 018 Ltd","as":"AS47956 XFone 018 Ltd","asname":"XFONE","reverse":"","mobile":false,"proxy":false,"hosting":false,"query":"141.226.27.42"}"

### Expiry Date

-   Allows users to set an expiry date for the shortened URL
-   Shortened URLs will no longer redirect after the expiry date has passed

### User Management

-   Mange Personal detals
-   Provides a dashboard for logged-in users to manage their shortened URLs
-   

### Statistics Page

-   Provides a comprehensive view of all shortened URLs
-   Includes data such as total clicks, unique clicks, and country of the user
-   **Provides a powerful data table with features such as search, search highlight, pagination, view records per page, remote refresh, refresh data, link details view, full screen, and more.**
- Indicate if Url has been expired 
- a Line Chart to view Links Locations with Counter 

## Technologies Used

-   C#
-   ASP.NET Core
-   Entity Framework Core
-   MSSQL
-   HTML/CSS
-   Bootstrap
-   jQuery
-   Chart.js

## Screen Shots
<table style="width:75%;height:75%;">
  <tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_36_39-.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_37_54-%E2%80%AAHome%20Page%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>
<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_38_16-%E2%80%AAHome%20Page%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_39_00-%E2%80%AAHome%20Page%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
	<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_39_43-%E2%80%AAStatistics%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_40_45-%E2%80%AAStatistics%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
	<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_41_14-%E2%80%AAStatistics%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_42_39-.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
	<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_42_57-%E2%80%AAProfile%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_44_04-%E2%80%AAPersonal%20Data%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
	<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_43_12-%E2%80%AAManage%20Email%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_43_24-%E2%80%AAChange%20password%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
	<tr>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_43_33-%E2%80%AAChange%20password%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
    <td>
      <img src="https://github.com/NaserElziadna/Shorty/blob/main/UrlShortener/ScreenShoots/2023-04-09%2011_43_50-%E2%80%AAConfigure%20authenticator%20app%20-%20UrlShortener.png" alt="drawing" style="width:100%;"/>    
    </td>
  </tr>	
</table>  

## Installation

### Prerequisites

-   Visual Studio 2019 or later
-   .NET Core SDK 3.1 or later
-   MySQL Server 8.0 or later
-   MySQL Workbench or any other MySQL client

### Setup

1.  Clone the repository to your local machine
2.  Open the solution file `UrlShortener.sln` in Visual Studio
3.  Open the `appsettings.json` file and set the `ConnectionStrings:DefaultConnection` value to your MySQL connection string
4.  Open the Package Manager Console and run `Update-Database` to apply the database migrations
5.  Build and run the application

## Usage

1.  Access the application using the URL `http://localhost:5000`
2.  Sign up for an account or log in with an existing account
3.  Shorten a URL using the "Shorten URL" button
4.  Use the shortened URL to redirect to the original URL
5.  View statistics for all shortened URLs on the "Statistics" page

## License

This project is licensed under the MIT License
