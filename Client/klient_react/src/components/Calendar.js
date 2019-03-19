import React, {Component} from 'react';
import moment from 'moment';
import '../styles/Calendar.scss';
import {DatetimePickerTrigger} from 'rc-datetime-picker';
import 'rc-datetime-picker/dist/picker.min.css';

class Calendar extends Component {
  constructor() {
    super();
    this.state = {
      moment: moment()
    };
  }

  handleChange = (moment) => {
    this.setState({
      moment
    });
    console.log(this.props);
    console.log(moment);
    this.props.onChange('dataFrom', moment)
  }

  render() {
    const shortcuts = {
      'Today': moment(),
      'Yesterday': moment().subtract(1, 'days'),
      'Clear': ''
    };

    return (
      <DatetimePickerTrigger
        shortcuts={shortcuts} 
        moment={this.state.moment}
        onChange={this.handleChange}
        className="calendar">
        <input type="text" value={this.state.moment.format('YYYY-MM-DD HH:mm')} id="dateFrom" readOnly />
      </DatetimePickerTrigger>
    );
  }
}

export default Calendar;