@negative
Feature: Negative scenarios for adding languages

  As a user
  I want to be prevented from adding invalid language entries
  So that my profile maintains valid data

  @negative
Scenario Outline: [Negative]Attempt to add a language with invalid or duplicate input
  Given User is on the Profile Page
  When User clicks on the Add New button under Languages
  And User enters "<language>" with "<level>"
  Then An appropriate error message should be displayed for "<errorType>"

  Examples:
    | language | level           | errorType             |
    |          | Fluent          | BlankLanguage         |
    | Hindi    |                 | BlankLevel            |
    | Telugu   | Native/Bilingual| DuplicateLanguage     |
    | English  |                 | InvalidLevelSelection |
    @negative   
  Scenario:[Negative] Prevent user from adding more than 4 languages
  Given User is on the Profile Page
  When User adds the following 4 languages:
    | language | level           |
    | Hindi    | Fluent          |
    | French   | Conversational  |
    | Telugu   | Native/Bilingual|
    | Spanish  | Basic           |
  And User attempts to add another language "Japanese" with "Fluent"
  Then The user should not be able to add a fifth language