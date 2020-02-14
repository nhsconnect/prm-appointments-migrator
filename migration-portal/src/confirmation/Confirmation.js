import React from 'react';
import { Link } from 'react-router-dom';
import { convertDate } from '../service/date';

const Confirmation = ({ confirmDeduction, data }) => {
  const { nhsNumber, patientName, dob, practice, telephone, email } = data;
  return (
    <div data-testid="confirmation">
      <dl className="nhsuk-summary-list">
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">NHS Number</dt>
          <dd className="nhsuk-summary-list__value">{nhsNumber}</dd>
        </div>
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">Name</dt>
          <dd className="nhsuk-summary-list__value">{patientName}</dd>
        </div>
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">Date of birth</dt>
          <dd className="nhsuk-summary-list__value">{convertDate(dob)}</dd>
        </div>
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">Current GP Practice</dt>
          <dd className="nhsuk-summary-list__value">{practice}</dd>
        </div>
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">Telephone</dt>
          <dd className="nhsuk-summary-list__value">{telephone}</dd>
        </div>
        <div className="nhsuk-summary-list__row">
          <dt className="nhsuk-summary-list__key">Email address</dt>
          <dd className="nhsuk-summary-list__value">{email}</dd>
        </div>
      </dl>
      <button className="nhsuk-button" onClick={() => confirmDeduction()}>
        Add to transfer list
    </button>
      <div className="nhsuk-back-link">
        <Link className="nhsuk-back-link__link" to="/home">
          <svg
            className="nhsuk-icon nhsuk-icon__chevron-left"
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            aria-hidden="true"
          >
            <path
              d="M8.5 12c0-.3.1-.5.3-.7l5-5c.4-.4 1-.4 1.4 0s.4 1 0 1.4L10.9 12l4.3 4.3c.4.4.4 1 0 1.4s-1 .4-1.4 0l-5-5c-.2-.2-.3-.4-.3-.7z" />
          </svg>
          Go back
      </Link>
      </div>
    </div>
  )
};

export default Confirmation;
