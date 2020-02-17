import React, { Fragment } from 'react';
import Header from '../components/header/Header';
import { parse } from 'query-string';
import Content from './content';
import { setFeatures } from '../config/features';
import { css } from 'emotion';

const App = () => {
  setFeatures(parse(window.location.search, { parseBooleans: true }));

  const content = css({
    maxWidth: '800px',
    margin: '0 auto',
    padding: '1rem'
  });

  return (
    <Fragment>
      <Header />
      <div className={content}>
        <Content />
      </div>
    </Fragment>
  )
};

export default App;
