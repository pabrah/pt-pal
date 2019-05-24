'use strict';

const e = React.createElement;

class Button extends React.Component {
    constructor(props) {
        super(props);
        this.state = { liked: false };
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
    getSchedule() {
        alert("button was clicked");
    }
}
const buttons = "<button id=SendSchedule onClick=callReg()>Send Schedule</button><br/><button id=GetSchedule onclick=getSchedule()>Get Schedule</button>"

const domContainer = document.querySelector('#buttons');
ReactDOM.render(buttons, domContainer);
