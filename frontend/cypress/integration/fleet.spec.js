/// <reference types="cypress" />

context('Fleet', () => {
  beforeEach(() => {
    cy.login();
  });
  it('should add new fleet', () => {
    cy.contains('Add new fleet').click();
    cy.get('input').eq(0).type('New fleet name{enter}');
    cy.url().should('not.eq', Cypress.config().baseUrl + '/fleets/new');
    cy.url().should('include', Cypress.config().baseUrl + '/fleets');
    cy.contains('New fleet name').should('exist');
  });
});
