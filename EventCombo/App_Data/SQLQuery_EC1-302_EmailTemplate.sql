GO
INSERT [dbo].[Email_Template] ([TemplateId], [Template_Name], [To], [CC], [Bcc], [Subject], [TemplateHtml], [From], [Template_Tag], [From_Name]) 
VALUES (N'fe6c39bd-7481-4083-a65f-2f326dca6896', N'Error Template', N'editor@Eventcombo.com', NULL, NULL, N'Error report', N'<p>PrecedingPage&nbsp;=&para;&para;PrecedingPage&para;&para;</p>

<p>ErrorPage&nbsp;=&para;&para;ErrorPage&para;&para;</p>

<p>SpecificErrorReason&nbsp;=&para;&para;SpecificErrorReason&para;&para;</p>
', NULL, N'email_error', N'Eventcombo')
GO
UPDATE [dbo].[Email_Template]
SET Template_Name='Error Template',
	[To]='editor@Eventcombo.com',
	[Subject]='Error report',
	[TemplateHtml]=N'<p>PrecedingPage&nbsp;=&para;&para;PrecedingPage&para;&para;</p>

					<p>ErrorPage&nbsp;=&para;&para;ErrorPage&para;&para;</p>

					<p>SpecificErrorReason&nbsp;=&para;&para;SpecificErrorReason&para;&para;</p>
					',
	[From_Name]=N'Eventcombo'
WHERE [Template_Tag]='email_error'
GO
