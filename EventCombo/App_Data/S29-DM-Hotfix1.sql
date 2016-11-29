SET IDENTITY_INSERT Email_Tag ON
INSERT INTO Email_Tag (Tag_Id, Tag_Name)
VALUES(48, 'UserPassword')
SET IDENTITY_INSERT Email_Tag OFF

GO

DELETE FROM [Email_Template]
WHERE Template_Tag = 'email_auto_register'
INSERT INTO [Email_Template] (TemplateId, Template_Name, [To], [Subject], TemplateHtml, Template_Tag)
VALUES ('2eba347f-e67b-4191-a4f5-b3f144ff5c2f', 
        'Notification about automatic registration Template', 
        '¶¶UserEmailID¶¶', 
        'Your registration on the Eventcombo.com',
        '<p><strong>Hi&nbsp; &para;&para;UserFirstNameID&para;&para; &para;&para;UserLastNameID&para;&para;,</strong></p><p>You was registered on the site <a href="http://¶¶ClickHere¶¶">eventcombo.com</a>. For sign in please use next credentials:</p><p>User name:&nbsp;&para;&para;UserEmailID&para;&para;</p><p>Password:&nbsp; &para;&para;UserPassword&para;&para;</p><p>Thank you for using of our site!</p>',
        'email_auto_register')