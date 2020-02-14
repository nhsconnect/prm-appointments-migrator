import React, { useState } from 'react';

export default ({ submitDeduction }) => {
  const [nhsNumber, setNhsNumber] = useState("");
  const [error] = useState("");

  const onSubmit = event => {
    event.preventDefault();
    submitDeduction(nhsNumber);
  };

  return (
    <div>
      <form data-testid="deduction-form" onSubmit={onSubmit} autoComplete="off">
        <div className="nhsuk-form-group">
          <label className="nhsuk-label" id="nhs-number-input" htmlFor="nhs-number">
            NHS Number
          </label>
          <input
            className={error ? "nhsuk-input nhsuk-input--error" : "nhsuk-input"}
            id="nhs-number"
            aria-labelledby="nhs-number-input"
            name="nhs-number"
            type="text"
            onChange={event => setNhsNumber(event.target.value)}
          />
        </div>

        <span className="nhsuk-error-message" data-testid="error">
          {error}
        </span>
        <button className="nhsuk-button" type="submit">
          Find patient
        </button>
      </form>
    </div>
  );
};
