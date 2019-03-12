import React, { Component } from 'react';
import '../css/Dashboard.scss';
import { withRouter } from 'react-router-dom';

class Dashboard extends Component {


    componentDidMount(){
        const { cookies } = this.props;
        if(cookies.get('token') === undefined){
          this.props.history.push('/');
          console.log("Token nie istnieje")
        }
      }

  render() {
    return (
      <div className="content">
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

        <div className="rooms">
            <h3>Sale:</h3>
            <div className="numbers">
                <p>Aktualnie wolne:</p>
                <span>3</span>
            </div>

            <div className="numbers">
                <p>Zajęte:</p>
                <span>0</span>
            </div>
        </div>

        <div className="meetings">
            <h3>Ostatnio rezerwowałeś:</h3><br/>
            <p>Jeszcze nie rezerwowałeś</p>
        </div>

        <div className="meetings">
            <h3>Statystyki rezerwacji:</h3><br/>
            <div className="data">
                <p className="data-p">ten tydzień:</p>
                <span className="data-s">0</span>

                <p className="data-p">poprz. tydzień:</p>
                <span className="data-s">0</span>
            </div>

            <div className="data">
                <p className="data-p">ten miesiąc:</p>
                <span className="data-s">0</span>

                <p className="data-p">poprz. miesiąc:</p>
                <span  className="data-s">0</span>
            </div>
        </div>
          
        </div>
    )
  }
}
export default withRouter(Dashboard);
