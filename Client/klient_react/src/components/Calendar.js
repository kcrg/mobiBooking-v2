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
    const { name } = this.props;
    this.setState({
      moment
    }, () => this.props.onChange(name, moment.format('YYYY-MM-DDTHH:mm')));
    
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
        <input type="text" value={this.state.moment.format('DD-MM-YYYY HH:mm')} id="dateFrom" readOnly />
      </DatetimePickerTrigger>
    );
  }
}

export default Calendar;