const express = require('express');

//declare express
const app = express();

//middleware to convert body to json 
app.use(express.json());

const port = 8080;

//to start, type node . in terminal 
//future feature, must start upon ARi project launch 

//mini database we will use to learn 
const courses = [ 
  {
    id: 1,
    dept: 'MATH',
    courseNum: 241,
    name: 'Calculus 1'},
  {
    id: 2,
    dept: 'CMPT',
    courseNum: 422,
    name: 'Organization and Architecture'},
  {
    id: 3,
    dept: 'CMPT',
    courseNum: 475,
    name: 'Capping'},
  {
    id: 4,
    dept: 'CMPT',
    courseNum: 308,
    name: 'DataBase management'},
];


//send courses to api 
//if status = 200 (connected) send courses to the API
app.get('/api/courses', (req, res) => {
  res.status(200).send(courses);
});

//URL For course with an ID of 1
//api/courses/1 
app.get('/api/courses/:id', (req, res) => {

  //since we are looking for an ID we must use parseInt
  let course = courses.find(c => c.id === parseInt(req.params.id));
  
  //if we fail to find a valid course
  if (!course){ res.status(404).send('The course was not valid.');}

  //if we have a valid course
  res.send(course);
});


//send a new course to the API using POST
//Also implement input validation
app.post('/api/courses', (req, res) => {

  //bad id request 
  if(!req.body.id){
    res.status(400).send('Name is required');
    return;
  }
  
  let course = {
    id: req.body.id,
    dept: req.body.dept, //use the name given in the API request
    courseNum: req.body.courseNum,
    name: req.body.name
  };
  courses.push(course); //add to our js data type (client)
  res.send(course); //add to api
});

/*example of a query parameter
Get all posts for a given month and given year
ADD A QUESTION MARK IN URL 
URL = http://localhost:8080/api/posts/2018/1?sortBy=name
*/
app.get('/api/posts/:year/:month', (req, res) => {
  res.send(req.query);
});


//${} will allow you to add a DYNAMIC VALUE
app.listen(port, () => console.log(`Server running on http://localhost:${port}`));
