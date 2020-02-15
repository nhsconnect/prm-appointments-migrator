import React, { Fragment } from 'react';
import Header from '../components/header/Header';
import styles from './App.module.scss';
import { parse } from 'query-string';
import Content from './content';
import { setFeatures } from '../config/features';

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
