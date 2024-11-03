![Arrival colorful brand banner.](./BookingManagementSystem/Assets/brand-banner-colorful.png)

<div class="row" align="center">
<a href='https://github.com/thatweirdbush/arrival'><img src='./BookingManagementSystem/Assets/get-it-on-github.png'alt='Get it from GitHub' height=80 /></a>
</div>

## Arrival – Hotel & Apartment Booking App
> A WinUI 3 application for managing hotel and apartment bookings and rentals.

## Introduction
Arrival is a modern and intuitive platform designed to streamline the booking process for hotels and apartments. Built with the latest WinUI 3 framework, the app provides users with a seamless experience to explore, book, and manage their stays with ease.

## Appearance & Design
Created with WinUI and the latest design ideologies, Arrival is modern while keeping all of the classic features people need. We use WinUI 3 to keep our user interface, clean, modern and consistent with Windows 11 UI and UX. Although, this app does work on Windows 10 too. 

We use all of your favourite material: Acrylic. We use Microsoft's icons and controls as well as some of our own to create a generally native feeling experience. Our own controls and icons give users a truly personalised experience, being able to choose their own icon packs and with features like compact mode, you can use it on any Windows device!

<table>
  <tr>
    <td width="50%"><image src='./BookingManagementSystem/Assets/Screenshots/Screenshot%20Signin%20Page.png'/></td>
    <td width="50%"><image src='./BookingManagementSystem/Assets/Screenshots/Screenshot%20Home%20Page.png'/></td>
  </tr>
  <tr>
    <td width="50%"><image src='./BookingManagementSystem/Assets/Screenshots/Screenshot%20Rental%20Details%20Page.png'/></td>
    <td width="50%"><image src='./BookingManagementSystem/Assets/Screenshots/Screenshot%20Listing%20Requests%20Page.png'/></td>
  </tr>
</table>
      
> [!NOTE]
> These screenshots may not contain all of the features shown in the app itself from GitHub. Some features are being tested internally.

## All Features
### **Basics**
- [x] **User Registration/Login/Password Recovery [3h] [21120060]** 
  - Allows users to create accounts, log in, and recover passwords for managing personal information and accessing services.
- [ ] **Booking/Cancellation of Accommodation [2h]**  
  - Diverse booking options for private homes, apartments, and hotels ranging from budget to premium.
- [ ] **Search for Rooms/Apartment by Schedule [2h]**  
  - Enables users to search for hotels and apartments by location, travel dates, and number of guests.
- [x] **Preset Filters [1h] [21120082]**  
  - Filters based on destination types, including Amazing Views, Beach, Farms, Islands, etc.
- [ ] **Advanced Filters [2h]**  
  - Filters by price, room type, guest capacity, amenities (Wi-Fi, air conditioning, pool), reviews, free cancellation, etc.
- [ ] **Discount Voucher Management [1h]**  
  - Allows discounts to be applied to the final payment amount.
- [x] **User Review Management [1h] [21120082]**  
  - Customers can leave a review about rooms/accommodations, helping improve service quality.
- [x] **Question and FAQ Management [2h] [21120082]**  
  - Handles user questions about rooms/accommodations and a FAQ section for common booking, payment, and cancellation inquiries.
- [ ] **Booking History Management [1h]**  
  - Stores users' past booking history.
- [ ] **Booking Confirmation via Email [2h]**  
  - Sends booking confirmations through email or in-app notifications.
- [x] **Report Abuse Management [1h] [21120082]**  
  - Allows users to report inappropriate behavior or accommodations that do not meet standards or match descriptions.
- [ ] **Loyalty Member Management [2h]**  
  - Loyalty program offering exclusive benefits such as vouchers and priority listings.
- [ ] **Amenity and Facility Tag Setup for Hosts [2h]**  
  - Hosts can create lists of tags for services and amenities their accommodations offer.
- [ ] **Email/App Notifications [2h]**  
  - Sends notifications for successful bookings, upcoming check-ins, and booking changes.
- [x] **Trending/Priority Listing Management [2h] [21120082]**  
  - Lists popular accommodations or those requested to be prioritized for a certain period.
- [ ] **Multilingual Support [2h]**
    - Allows users to select the application language.

### **Advance**
- [ ] **Live Chat with Host/Staff/Bot [1h]** 
  - Chat system supports live chat with host, hotel staff or bot before booking.
- [ ] **Travel Suggestions [1h]** 
  - Personalized travel suggestions by AI.
- [x] **Map integration [1h] [21120082]** 
  - Display room/apartment location on map, helps it easy to locate.

 > ### Total Works Need To Be Done: **6h (for 2 members)**
 > ### Total Works Completed: **11h**
 > ### Total By Assignment Per Member:
 > - **[21120060] Nguyen Long Giang: 3h**
 > - **[21120082] Phan Quoc Huy: 8h**


# Assessment
## UI/UX
- **Modern, clean and consistent interface**
- **Using latest WinUI 3 design ideologies:** Fluent Design with Acrylic translucent material
- **Input validation:** Validate empty string and white spaces in all string `TextBox`
  - **User Report Form:** `Reason` & `Description` text boxes
  - **Review Form:** `Your Comment` text box
  - **Rental Details Page:** `Ask question` text box
  - **Sign In Page:** `Username`, `Password` text boxes
  - **Sign Up Page:** `Username`, `Password`, `Confirm Password` text boxes
  - **Reset Password Page:** `Username`, `Password`, `Confirm Password` text boxes
  - **Auto Suggestion Search Box:** `Search Destination` in Home Page, `Navigation Item` in Shell Page
 
 > [!NOTE]
 > ### Self-Assessment: 20/20 (%)
 
## Design Patterns & Architecture
- **MVVM Architecture**
  - This helps separate the application logic from the user interface. ViewModels like `HomeViewModel`, `RentalDetailsViewModel`, etc. are registered and bound to the respective Pages.
- **Service Locator**
  - `GetService<T>()` method helps retrieve the service from the Host.
- **Dependency Injection (DI)**
  - Using `Host` and `ConfigureServices` method to register services, DI pattern helps to separate dependencies and increase flexibility in configuring and replacing services.
  - Services are registered with different scopes (`Singleton`, `Transient`) to ensure appropriate lifecycle for each type of object.
- **Factory Method**
  - In `ConfigureServices`, services and ViewModels are registered with object creation methods (`AddTransient`, `AddSingleton`). This structure allows flexibility in creating new objects or reusing singleton objects.
- **Page Service**
  - The `PageService` (inherited from `IPageService`) is responsible for managing the mapping between ViewModel and Page. The `Configure<VM, V>()` method is used to configure ViewModel and Page pairs, ensuring that each ViewModel has a corresponding Page.
- **Lifecycle-aware Component**
  - The `INavigationAware` interface defines methods related to navigation. When a ViewModel implements this interface, it provides logic for handling navigation events, such as moving to a page (`OnNavigatedTo`) or leaving a page (`OnNavigatedTo`).
 
 > [!NOTE]
 > ### Self-Assessment: 20/20 (%)

## Advanced Topics
- **Acrylic System Backdrops**
  - Provide a different background for a Window other than the default white or black (depending on Light or Dark theme)
  - Desktop Acrylic are the current supported backdrops
- **Dark Theme**
  - This application supports Light and Dark themes as a personalization option in application settings
  - We use Light mode by default, but users can choose Dark mode, which changes much of the UI to a dark color
- **Attached Drop Shadow**
  - Allows many elements to share a common backdrop for casting shadows
  - Applied to `Schedule Search Bar` in Home Page
- **Connected Animations**
  - Created a dynamic and compelling navigation experience by animating the transition of an element between two different views
  - Applied to the Item Thumbnail named `itemHero` in Home Page & Rental Details Page
- **Auto Suggest Box**
  - A text control that makes suggestions to users as they type. The app is notified when text has been changed by the user and is responsible for providing relevant suggestions for this control to display
  - Applied to `Search Destination` in Home Page and `Navigation Item` in Shell Page
- **Inverse Background Brush**
  - A Background theme resource that inverts the control's background brush bases on the background of the application
  - Applied to several buttons like `Book Now`, `Submit Question`
- **Auto Scroll to Top**
  - Helps scroll to top of the main `ScrollView` when repeatedly navigate to a page
  - Optimize page resources when reloading
  - Applied to Rental Details Page
- **Adaptive Grid View**
  - This control presents items in a evenly-spaced set of columns to fill the total available display space. It reacts to changes in the layout as well as the content so it can adapt to different form factors automatically.
  - Applied to Home Page & Rental Details Page
- **Flip View & Pips Pager**
  - `FlipView` allows flipping through a collection of items, one at a time. It's great for displaying images from a gallery, pages of a magazine, or similar items.
  - `PipsPager` allows the user to navigate through a paginated collection and is independent of the content shown.
  - Applied to every image collection views
 
 > [!NOTE]
 > ### Self-Assessment: 30/30 (%)

## Teamwork - Git Flow
### Project Management With Trello
![Project Management With Trello.](./BookingManagementSystem/Assets/Screenshots/Screenshot%20Project%20Management%20Trello.png)

### Git Flow
![Git Flow.](./BookingManagementSystem/Assets/Screenshots/Screenshot%20Git%20Flow.png)
 
 > [!NOTE]
 > ### Self-Assessment: 10/10 (%)

## Quality Assurance
### Source Code Review & Merg Into Main Process
![Source Code Review & Merg Into Main Process.](./BookingManagementSystem/Assets/Screenshots/Screenshot%20Pull%20Requests.png)
![Source Code Review & Merg Into Main Process.](./BookingManagementSystem/Assets/Screenshots/Screenshot%20Pull%20Requests%202.png)
 
 > [!NOTE]
 > ### Self-Assessment: 15/20 (%)

> [!IMPORTANT]
> ### Total Completion Rate: 95/100 (%)

# Meet our contributors
<a href="https://github.com/thatweirdbush/arrival/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=thatweirdbush/arrival"/>
</a>

<hr>
<h6 align="center">© Arrival. 2024 
<br>
All Rights Reserved</h6>
