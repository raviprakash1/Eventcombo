
delete from User_Permission_Detail  where UP_Permission_Id in (1,2)

insert into User_Permission_Detail 
(Up_User_Id,Up_Permission_Id) 
select Id,1  From AspNetUsers

insert into User_Permission_Detail 
(Up_User_Id,Up_Permission_Id) 
select Id,2  From AspNetUsers


Update Profile set Organiser ='Y'
