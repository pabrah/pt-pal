'use strict';

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
        fetch('https://pt-pal.azurewebsites.net/api/RegisterTrainingSchedule',
        {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(
            {
                Owner: 'Petter',
                ExerciseDays: [
                {
                    Day: 1,
                    ExercisesForToday: [{
                        Name: "Bench Press"
                    }]
                },
                {
                    Day: 2,
                    ExercisesForToday: [{
                        Name: "Interval Running Indoor"
                    }]
                }]
            }),
        })
    }
}

const domContainer = document.querySelector('#SendSchedule');
ReactDOM.render(e(LikeButton), domContainer);