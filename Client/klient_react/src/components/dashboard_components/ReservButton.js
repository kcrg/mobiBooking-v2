import React, { Component } from 'react';
import '../../styles/dashboard_comp/ReservButton.scss';
import { withRouter } from 'react-router-dom';

class ReservButton extends Component {

  navigateTo = () =>{
    this.props.history.push('/roomReserv')
  }
  
  render() {
    return (
        <div className="reserv_button" onClick={this.navigateTo}>
            <span>Zarezerwuj salę</span>
        </div>
    )
  }
}

export default withRouter(ReservButton)
