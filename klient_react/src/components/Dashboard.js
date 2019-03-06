import React, { Component } from 'react';
import '../css/Dashboard.scss';

export default class Dashboard extends Component {
  render() {
    return (
      <div id="content">
        <div className="messeges">
            <h3>Moje spotkania:</h3>
            <div className="data">
                <p>ten tydzień</p>
                <span>0.00h</span>

                <p>poprz. tydzień</p>
                <span>0.00h</span>
            </div>

            <div className="data">
                <p>ten miesiąc</p>
                <span>0.00h</span>

                <p>poprz. miesiąc</p>
                <span>0.00h</span>
            </div>
        </div>

        <div className="meetings">
            <h3>Spotkania na dziś:</h3><br/>
            <p>Nie masz żadnych spotkań</p>
        </div>

        <div className="meetings">
            <button>
                Zarezerwuj salę
            </button>
        </div>
      
        </div>
    )
  }
}
