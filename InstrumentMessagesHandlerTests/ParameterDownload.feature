Feature: ParameterConfirmation
	
Scenario: Get parameter list
	Given a connected Data Manager
	And a connected Main Unit
	When Main Unit sends a PL01 message
	Then Data Manager sends an RO00 confirmation message
	And Data Manager requests to download the List of Parameters to TSNAgent

Scenario: Send parameter List to CU
	Given a connected Data Manager
	And a connected Main Unit
	When Data Manager receives the List of Parameters from TSNAgen
	Then Data Manager sends a PL11 message to the Main Unit
	And Data Manager sends an xml file with th name ListOfApp.xml with all the parameters
	And the Main Unit sends an RO00 Message to confirm the reception

Scenario: Get parameter
	Given a connected Data Manager
	And a connected Main Unit
	When Main Unit sends a PD01 message
	Then Data Manager sends an RO00 confirmation message
	And Data Manager requests to download the parameter to TSNAgent

Scenario: Send parameter to CU
	Given a connected Data Manager
	And a connected Main Unit
	When Data Manager receives the parameter from TSNAgent
	Then Data Manager sends a PD11 message to the Main Unit
	And Data Manager sends an xml file with th name Application.xml with the parameters
	And the Main Unit sends an RO00 Message to confirm the reception

Scenario: Confirm parameter
	Given a connected Data Manager
	And a connected Main Unit
	When Main Unit sends an AP01 message
	And Main Unit sends an xml with the name Confirmation.xml containing the parameter to confirm
	Then Data Manager sends an RO00 confirmation message
	And Data Manager stores the parameter
