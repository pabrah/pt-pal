'use strict';

const e = React.createElement;

class Button extends React.Component {
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
        return f(
            'button', {
                onClick: () => this.getSchedule()
            },
            'GetSchedule'
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
ReactDOM.render(e(Button), domContainer);
const getSchedule = document.querySelector('#GetSchedule');
ReactDOM.render(f(Button), getSchedule);