import React, { Fragment } from 'react';
import Header from '../header/Header';
import styles from './App.module.scss';
import { parse } from 'query-string';
import Content from './Content';
import { setFeatures } from '../features';

const App = () => {
  setFeatures(parse(window.location.search, { parseBooleans: true }))

  return (
    <Fragment>
      <Header />
      <div className={styles.content}>
        <Content />
      </div>
    </Fragment>
  )
};

export default App;
