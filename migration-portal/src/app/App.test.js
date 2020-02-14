import React from 'react';
import { MemoryRouter } from "react-router-dom";
import TestRenderer from 'react-test-renderer';
import App from './App';

describe('<App />', () => {
  it('should render the header', async () => {
    const testRenderer = TestRenderer.create(
      <MemoryRouter>
        <App />
      </MemoryRouter>);
    const testInstance = testRenderer.root;

    expect(testInstance.findByType('header')).toBeTruthy();
  });
});



