UPDATE Email_Template
SET TemplateHtml='<table border="0" cellpadding="0" cellspacing="0" style="width:100%">
	<tbody>
		<tr>
			<td>
			<table>
				<tbody>
					<tr>
						<td><a href="¶¶CreateEventurl¶¶">Create Event</a> <a href="¶¶DiscoverEventurl¶¶">Discover Events</a> <a href="¶¶MyTicketEventurl¶¶">My Tickets</a></td>
					</tr>
				</tbody>
			</table>

			<table style="width:100%">
				<tbody>
					<tr>
						<td>
						<table>
							<tbody>
								<tr>
									<td>
									<table>
										<tbody>
											<tr>
												<td><img src="http://eventcombonew-qa.kiwireader.com/Images/logo_vertical.png" /></td>
												<td>
												<h1>Hi &para;&para;UserFirstNameID&para;&para;&nbsp;&nbsp;, this is your e-ticket confirmation for &para;&para;EventTitleId&para;&para; Organized by &para;&para;EventOrganiserName&para;&para;&nbsp;</h1>

												<p>Organized by &nbsp;&para;&para;EventOrganiserName&para;&para;&nbsp;</p>

												<p>&nbsp;</p>

												<h1>Get tickets :</h1>

												<p style="text-align:center"><img src="http://eventcombonew-qa.kiwireader.com/Images/paper_tic.png" /><br />
												Open the email attachement<br />
												or <a href="¶¶Downloadurl¶¶">download here </a></p>

												<h1>Question about this event ?</h1>

												<p>Contact the organizer at&nbsp;<a href="#">&nbsp;&para;&para;EventOrganiserEmail&para;&para;&nbsp;</a></p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>&para;&para;EventDynamicTable&para;&para;</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%">
												<h1>About this event</h1>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/em_ven_time.png" />&para;&para;EventStartDateID&para;&para;</p>

												<p style="text-align:center">To</p>

												<p>&para;&para;EventEndDateID&para;&para;</p>

												<p>Venue Name</p>

												<p><img src="http://eventcombonew-qa.kiwireader.com/Images/email_loc_icn.png" /> &para;&para;EventVenueID&para;&para;</p>
												&para;&para;EventMapImage&para;&para;

												<p>&para;&para;Eventtype&para;&para;</p>

												<p>&nbsp;</p>
												</td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td style="width:100%"><img src="http://eventcombonew-qa.kiwireader.com/Images/login_img.png" />
												<h1>Your Account</h1>

												<p><a href="¶¶EventLogin¶¶">Log in </a> to access tickets and manage your orders.</p>
												</td>
											</tr>
										</tbody>
									</table>
									</td>
								</tr>
							</tbody>
						</table>
						</td>
					</tr>
				</tbody>
			</table>
			</td>
		</tr>
	</tbody>
</table>
'
WHERE Template_Tag='eticket' 

