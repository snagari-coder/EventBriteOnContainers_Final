# EventBriteOnContainers
 EventBriteOnContainers

PROJECT TITLE: MVC Client for Eventbrite, including Token Service, Cart Service and Order Service

PROJECT DESCRIPTION

Built MVC client for Eventbrite. User-interface (view) was added to complete the design pattern with data (model) and application logic (controllers). Views included show-events (image, date, time and place) and pagination. Controllers included on MVC client are Account Controller, Catalog Controller and Home Controller.

All our microservice use ASP .NET core.

TokenService Microservice: This microservice is to authenticate and authorize users, with login and logout concepts ( using SQL server db). This was not built from scratch, we are reusing what is being used in the industry. In this phase, we included authentication service as a security token service using Token Service Microservice and Redis cache. A database is created to store user names and passwords and we seed it with the user me@myemail.com.

CartApi Microservice. The basket service allows the user to add items to cart after logging in, via the TokenService. The basket service caches the items that the user added previously, and saves it under the BuyerId. The CartApi contains the CartItem information, such as Id, ProductId, ProductName, UnitPrice, OldUnitPrice, Quantities, Picture. These CartItems are then listed in the cart and the total sum of the cart is displayed at the bottom.

OrderApi Microservice. The order service, so that the user can purchase the list of items in cart. The OrderService is integrated with the TokenService, which authenticates the user and authorizes the OrderService to store the user’s information such as, Name, OrderDate, BuyerId, OrderStatus, Address, Payment, OrderTotal, and list of OrderItems.

Each microservice is containerized/Dockerized. Communication between all the microservices is done by EventBus called RabbitMQ.We implemented the Stripe program to process the credit card transaction.

UI allows the user to see all the events on database and filter by host and type of event as well as pagination. It is possible to login as the user: me@myemail.com and create new users.Images such as logo of project and images for banner are stored on wwwroot folder on Mvc. For authentication we add account controller, home Controller and manage controller as well as new model to manage errors on UI (ErrorViewModel).

Docker-compose file includes new containers to run Token Service, Cart Service and Order Service. We are using the same sqlserver container as CatalogEvent for demonstration purposes although it's possible create a different one for each service. To process a card transaction we integrated Stripe so it is possible to use real credit card information in a furure.

KEY FEATURES:

UI allows users to see all events and filter by host or/and type of event.
Users can authenticate using login.
Users can create new accounts to login.
Users can create orders with all events linked to their accounts (Token service)
Order information is storage in a Database.
Stripe (Stripe.com) is integrated to process credit card transaction on the checkout of an order.
RabbitMQ is integrated to queue up the order messages to be processed.

https://www.youtube.com/watch?v=Sub7ss9F-P0
