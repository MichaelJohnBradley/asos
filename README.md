# asos

Hi
I have taken just the 2 hours to do the attached. I have added xUnit and Moq for the unit testing and I have added some unit tests that indicate how I have used these frameworks previously.
The first thing I noticed in the AddCustomer method was the number of parameters, so I made that a task to refactor that to reduce the number.
I have extracted out a few other methods from the AddCustomer method and put them in their own methods – these most likely belong somewhere else – and maybe on the customer or company classes in a domain driven design way but having not done any domain driven design before I didn’t want to spend a lot of time rushing it and making a mess of it.
To maintain compatability with the harness project I introduced an overloaded method and then also focused on applying some dependency injection principles to the solution. 
The solution I have submitted is not in a “finished” status as there would have been several other things to look at. I have also made a couple of comments in the CustomerService class.
