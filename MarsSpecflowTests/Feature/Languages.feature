Feature: Manage Languages on Profile Page

  As a user
  I want to add, edit, and delete languages on my profile
  So that my profile reflects my language skills

  @tag1
  Scenario Outline: Add a new language to the profile
    Given User is on the Profile Page
    When User clicks on the "Add New" button under Languages
    And User enters "<language>" with "<level>"
    And User clicks on the "Add" button
    Then The language "<language>" should be added to the profile

    Examples:
      | language | level             |
      | Hindi    | Native/Bilingual  |
      | French   | Conversational    |
