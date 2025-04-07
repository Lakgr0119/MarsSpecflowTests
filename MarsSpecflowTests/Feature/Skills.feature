@skills
Feature: Manage Skills section

  As a user
  I want to manage my skills in the profile section
  So that I can showcase my expertise accurately

  @positive
  Scenario: Add a new skill successfully
    Given User is on the Profile Page
    When User navigates to Skills tab
    And User adds a new skill "Automation Testing" with level "Expert"
    Then The skill "Automation Testing" with level "Expert" should be visible in the Skills table

  @positive
  Scenario: Edit an existing skill
    Given User is on the Profile Page
    When User navigates to Skills tab
    And User edits the skill "Automation Testing" to "API Testing" with level "Intermediate"
    Then The skill "API Testing" with level "Intermediate" should be visible in the Skills table

  @positive
  Scenario: Delete a skill
    Given User is on the Profile Page
    When User navigates to Skills tab
    And User deletes the skill "API Testing"
    Then The skill "API Testing" should not be present in the Skills table