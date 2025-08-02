# Bankomat simulator

Current repository consists of OOP-labs written in C# language (IS ITMO y27). Last, the fifth lab - the most interesting one, is about creating an ATM-service. To implement it, I used docker container & stored data through PostgresSQL DBMS.

List of commands:

log in [user/admin] [accountId PIN/password] - log in (sign in)
top up [amount] - top up the bank account (which is currently viewed)
withdraw [amount] - (withdraw money from bank account (which is currently viewed)
show balance {accountid - available for admin only} - show account balance (admin can see balance of any account)
see history - show transactions history
create user {username} - create new user (you need to be an admin can do this))
create account [userId] [PIN] - create new account (you need to be an admin to do this)
disconnect - disconnect from the system, log out
