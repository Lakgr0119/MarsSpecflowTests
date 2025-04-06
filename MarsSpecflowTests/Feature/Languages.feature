@positive
Feature: Languages

As a user, I want to add my languages and skills to my profile  
So that people can see my expertise.  
Adding, editing, and deleting languages in user profile


@ignore
Scenario Outline: Verify login with different valid credentials
  Given User is on the login page and enters "<username>" and "<password>"
Examples: 
  | username       | password |
  | anya@gmail.com |	123123   |


@order1 @positive
Scenario Outline:[Positive] Add a new language to the profile
  Given User is on the Profile Page
  When User clicks on the Add New button under Languages
  And User enters "<language>" with "<level>"
  Then The language "<language>" should be added to the profile

Examples:
  | language | level          |
  | Hindi    | Fluent         |
  | French   | Conversational |



  @order2  @positive
  Scenario Outline: [Positive]Edit an existing language
  Given User is on the Profile Page
  When User edits the language "<oldLanguage>" to "<newLanguage>" with "<newLevel>"
  Then The language "<newLanguage>" should be added to the profile

  Examples:
    | oldLanguage | newLanguage | newLevel       |
    | Hindi      | Telugu      | Native/Bilingual |

@order3 @positive
Scenario Outline: [Positive]Delete a language from the profile
  Given User is on the Profile Page
  When User deletes the language "<language>"
  Then The language "<language>" should be removed from the profile

Examples:
  | language |
    | French   |
    






