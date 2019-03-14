import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

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
            <div>
                <div>
                    <h3>Moje spotkania:</h3>
                    <div>
                        <p>ten tydzień</p>
                        <span>0.00h</span>

                        <p>poprz. tydzień</p>
                        <span>0.00h</span>
                    </div>

                    <div>
                        <p>ten miesiąc</p>
                        <span>0.00h</span>

                        <p>poprz. miesiąc</p>
                        <span>0.00h</span>
                    </div>
                </div>

                <div>
                    <h3>Spotkania na dziś:</h3><br/>
                    <p>Nie masz żadnych spotkań</p>
                </div>

                <div>
                    <button onClick={this.buttonClick}>Zarezerwuj salę</button>
                </div>

                <div>
                    <h3>Sale:</h3>
                    <div>
                        <p>Aktualnie wolne:</p>
                        <span>3</span>
                    </div>

                    <div>
                        <p>Zajęte:</p>
                        <span>0</span>
                    </div>
                </div>

                <div>
                    <h3>Ostatnio rezerwowałeś:</h3><br/>
                    <p>Jeszcze nie rezerwowałeś</p>
                </div>

                <div>
                    <h3>Statystyki rezerwacji:</h3><br/>
                    <div>
                        <p>ten tydzień:</p>
                        <span>0</span>

                        <p>poprz. tydzień:</p>
                        <span>0</span>
                    </div>

                    <div>
                        <p>ten miesiąc:</p>
                        <span>0</span>

                        <p>poprz. miesiąc:</p>
                        <span>0</span>
                    </div>
                </div>
            </div>
        )
    }
}
export default withRouter(Dashboard);
