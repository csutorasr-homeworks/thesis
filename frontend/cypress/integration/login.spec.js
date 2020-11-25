/// <reference types="cypress" />

context('Login', () => {
  beforeEach(() => {
    cy.visit('/');
  });
  it('should redirect to login', () => {
    cy.url().should('eq', Cypress.config().baseUrl + '/account/login');
  });

  it('should redirect from login after entering email', () => {
    cy.get('input').type('test@test.com{enter}');
    cy.url().should('not.eq', Cypress.config().baseUrl + '/account/login');
  });

  it('should be able to login', () => {
    cy.get('input').type('test@test.com{enter}');
    cy.url().should('not.eq', Cypress.config().baseUrl + '/account/login');
    cy.get('input').eq(0).type('csutorasr@gmail.com');
    cy.get('input').eq(1).type('!23QWEasd{enter}');
    cy.url().should('eq', Cypress.config().baseUrl + '/');
  });
});
