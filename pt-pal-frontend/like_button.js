'use strict';
import axios from 'axios'

const e = React.createElement;

class LikeButton extends React.Component {
  constructor(props) {
    super(props);
    this.state = { liked: false };
  }

    render() {
        return e(
            'button',
            { onClick: () => this.callReg() },
            'SendSchedule'
        );
    }
    callReg() {
        axios.post('https://pt-pal.azurewebsites.net/api/RegisterTrainingSchedule', '"ExerciseWeekSchedule":{"Owner":"Petter", "ExerciseDays":[{"Day":1, "ExercisesForToday":{"Name":"Bench Press"}}]}').then(response => console.log(response))
    }
}

const domContainer = document.querySelector('#SendSchedule');
ReactDOM.render(e(LikeButton), domContainer);