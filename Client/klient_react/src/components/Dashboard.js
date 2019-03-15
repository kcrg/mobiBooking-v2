import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import '../styles/Dashboard.scss';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMugHot } from '@fortawesome/free-solid-svg-icons';

library.add(faMugHot)

class Dashboard extends Component {

    componentDidMount(){
        const { cookies } = this.props;
        if(cookies.get('token') === undefined){
            this.props.history.push('/');
        }
    }

    buttonClick = () =>{
        this.props.history.push('/roomReserv');
    }

    render() {
        return (
            <div className="dashboard-content">
                <div className="data meetings">
                    <h2>Moje spotkania:</h2>
                    <div className="meetings">
                        <span>Ten tydzień</span>
                        <p>0.00h</p>
                        <span>poprz. tydzień</span>
                        <p>0.00h</p>
                    </div>
                    <div className="meetings">
                        <span>ten miesiąc:</span>
                        <p>0.00h</p>
                        <span>poprz. miesiąc</span>
                        <p>0.00h</p>
                    </div>
                </div>

                <div className="data">
                    <div className="today_meetings">
                        <h2>Spotkania na dziś:</h2>
                        <span>W dzisiejszym dniu nie masz spotkań</span>
                        <FontAwesomeIcon icon={faMugHot}></FontAwesomeIcon>
                    </div>
                </div>

                <button onClick={this.buttonClick} className="data_btn">Zarezerwuj salę</button>

                <div className="data">
                    <div className="rooms">
                        <h2>Sale:</h2>
                        <span>Aktualnie wolne:</span>
                        <p>3</p>
                        <span>Aktualnie zajęte:</span>
                        <p>0</p>
                    </div>
                </div>

                <div className="data">
                   <div className="lastReservations">
                       <h2>Ostatnio rezerwowałeś/aś:</h2>
                       <span>Jeszcze nie rezerwowałeś</span>
                   </div>
                </div>

                <div className="data">
                    ONE
                </div>
            </div>
        )
    }
}
export default withRouter(Dashboard);
