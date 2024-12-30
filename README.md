# oop-assignment-b-73348

Banking App
Intro

The Banking Application is a console application in C#, that started as a blank solution application that models a banking system for employees and customers. I lowkey developed it as a personal learning project to learn about programming concepts while trying to challenge myself to learn through practice with almost no external help.

Features and Functionality

The app has two main categories of users: bank employees and customers, with functionalities tailored to each.

Employee Features

Employees log in with a PIN and can do account management for customers using a menu. The primary features of the employees are listed below:

New Customer Creation: This feature generates a unique account number for each customer using their name, creates separate files for savings and current accounts, and stores details in a centralized file.

Delete Customer: Employees can delete a customer's record if and only if their savings and current account balances are zero thereby performing responsible closures of accounts. The system updates the central customer file and removes corresponding account files.

Performing Transactions: Employees can perform deposits and withdrawals on behalf of customers, specifying the account type (savings or current). However, the current implementation lacks full error handling to ensure seamless operation.

Features for the Customers

Customers can log in using their account number and a PIN, which is generated with base on their account number. Once authenticated, they can perform banking tasks through the features:

Deposit Money: The customer can deposit money into either the current or savings account, and the system updates the balance.

Withdraw Money: Customers can withdraw money if the account balance is sufficient, where the system does not allow the balance to go negative.

Challenges Faced

File Handling: The operations of creating, reading, updating, and deleting files were difficult to manage. The big issue was ensuring accurate access and updates while maintaining data integrity.

Data Validation: Validating inputs such as account numbers, PINs, and transaction amounts required very careful consideration. Edge cases, such as invalid files, made development even more challenging.

Naming Issues: The naming of the project file caused a big delay. I first named the console app "Banking.Console" according to the guidelines, but I had several configuration errors. After 20 minutes of troubleshooting, I renamed it to "BankApp," which fit the structure better and fixed the errors. This experience underlined the importance of consistent naming and organization of a project.

Balancing Simplicity and Functionality: Since I had to choose between the core features and the advanced ones, I opted for the former to make the code manageable.

Coding Requirements: There were also quite challenging coding requirements set by the professor.

Interfaces and Inheritance: I made good use of classes and methods but was restricted on using interfaces and inheritance by the time and focus on core functions.

Testing: A testing module wasn't implemented. I did test manually, but automated tests would have improved reliability.

File Naming: Account and transaction files followed guidelines but the error handling for such conditions as naming conflicts or corrupted files was underdeveloped.

Conclusion

The Banking Application project really helped in applying the concepts of programming and deepening my understanding of file handling, data validation, and object-oriented design. Although some features are not implemented, I am proud of what I have done so far and the challenges I have taken on. This project speaks to my commitment toward learning and problem-solving on my own.

The Banking Application is a base for future development. With more effort, it can become a polished application—one that really shows technical skill and a commitment to learning.
