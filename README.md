# A simple use of [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five) with [Angular 8](https://indepth.dev/posts/1076/whats-new-after-angular-8)

## Website currently down ~~View the website [here](https://csoliberty20210516163403.azurewebsites.net/)~~

## Database consists of **Clients**, **Sellers** and **Orders**
    Clients stores id, first name, and last name of each client
    Sellers stores id, first name, last name, and id of the boss of each seller
    Orders stores id, client id, seller id, cost and date of each order

## Website consists of a single page that has a tab control with 3 sides: 
    1. For showing a table of orders
    2. For getting last order in given time period
    3. For showing a tree-view hierarchy of sellers

There is a static `DbInit` class inside folder **Data** that populates the database with dummy values.

## We have 4 *.sql* files inside folder **SQL**:
1. `CycleChecker.sql` is a function that returns 1 if given *id* of a **Seller** is its own boss through a long cycle (which is an error) and returns 0 otherwise
2. `SellerOwnBossTrigger.sql` creates a trigger that finds all empty *seller_boss_id* fields and assigns their own *seller_id* to them.
3. `SellerCycleTrigger.sql` creates a trigger that invokes the `CycleChecker.sql` and [raises an error](https://docs.microsoft.com/en-us/sql/t-sql/language-elements/raiserror-transact-sql) if a cycle is found
4. `OrderDepthTrigger.sql` creates a trigger that ensures that **Order** hierarchy does not go deeper than 2 levels
