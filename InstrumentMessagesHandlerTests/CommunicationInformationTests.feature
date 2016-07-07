Feature: Communication Information Messages
	
Scenario: Urgent information request receive UR01
	Given a connected Data Manager
	And a disconnected Main Unit
	When Data Manager receives an UR01 message
	Then Data Manager sends an RO00 message to confirm the reception
	
Scenario: Urgent information request handle UR01
	Given a connected Data Manager
	And a disconnected Main Unit
	When Data Manager receives an UR01 message
	Then Data Manager requests to download the urgent information to TSNAgent
	
Scenario: Urgent information request sends UR11
	Given a connected Data Manager
	And a disconnected Main Unit
	When Data Manager receives the urgent information from TSNAgent as an xml file	
	Then Data Manager sends an UR11 message to the Main Unit with the xml file	
	And Data Manager expects to receive a RO00 to confirm the reception