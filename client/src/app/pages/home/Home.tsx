import { useEffect, useState, useContext } from 'react';
import { useMqtt } from '@ShareCores';
import { EventUtil } from '@ShareUtils';
import { useSelector } from 'react-redux';
import { RootState } from '@ShareStores';
import { Grid, TextField, MenuItem, List, ListItem, ListItemText, Typography, Button } from '@mui/material';
import { useGetListQuiz, useGetListQuestionByQuizId, useSubmitAnswer } from '@ShareHooks';
import { QuestionComponent, RankList } from '@ShareUis'

export const Home = () => {
  const [isStartQuiz, setIsStartQuiz] = useState(false);
  const [isQuizSelected, setIsQuizSelected] = useState(false);
  const [count, setCount] = useState(0);
  const [limitTimeInSecond, setLimitTimeInSecond] = useState(0);
  const currentUserName = useSelector((state: RootState) => state.GlobalVar.currentUserName);

  const [core, setCore] = useState(0);

  const [selectedQuizId, setSelectedQuizId] = useState('');

  const [topic, setTopic] = useState('testtopic');
  const { messages, setMessages } = useMqtt(topic);


  const { GetListQuiz, ouput: quizResult } = useGetListQuiz();
  const { GetListQuestionByQuizId, ouput: questionsResult } = useGetListQuestionByQuizId();
  const { SubmitAnswer, ouput: answerResult } = useSubmitAnswer();

  const handleButtonStartQuizClick = () => {
    if (!isStartQuiz) {
      setCount(0);
    }
    setIsStartQuiz(!isStartQuiz);
  };

  EventUtil.useBackgroudJob(1, () => {
    if (isStartQuiz) {
      if ((limitTimeInSecond > 0) && (limitTimeInSecond > count)) {
        setCount(count + 1);
      }
      if (limitTimeInSecond <= count) {
        setIsStartQuiz(false);
      }
    }
  });

  const getLimitTimeInSecondQuizById = (quizId: string) => {
    var temp = quizResult.find(quiz => quiz.id === quizId);
    if (temp && temp.limitTimeInSecond) {
      setLimitTimeInSecond(temp.limitTimeInSecond)
    }
  };

  const handleQuizChange = (event: any) => {
    const quizId = event.target.value;
    setSelectedQuizId(quizId);
    GetListQuestionByQuizId({ quizId });
    setTopic(`${quizId}_Rank`);
    getLimitTimeInSecondQuizById(quizId)
    setIsQuizSelected(true);
    setCore(0);
    setCount(0);
    setIsStartQuiz(false);
    setMessages([]);
  };

  const handleSubmitAnswer = (questionId: string, answer: string) => {
    SubmitAnswer({
      questionId,
      quizId: selectedQuizId,
      userFullName: currentUserName,
      answer,
      currentCore: core,
      currentTimeSecond: count
    });
  };

  useEffect(() => {
    GetListQuiz();
  }, [GetListQuiz]);

  useEffect(() => {
    if (answerResult && answerResult.core !== undefined) {
      setCore(answerResult.core);
    }
  }, [answerResult]);

  return (
    <>
      <Grid container spacing={2}>
        <Grid container spacing={2} alignItems="center">
          <Grid item xs={11}>
            <TextField
              select
              label="Select Quiz"
              fullWidth
              value={selectedQuizId}
              onChange={handleQuizChange}
            >
              {quizResult.map((quiz) => (
                <MenuItem key={quiz.id} value={quiz.id}>
                  {quiz.name}
                </MenuItem>
              ))}
            </TextField>
          </Grid>
          <Grid item xs={1}>
            <Button
              variant="contained"
              color={isStartQuiz ? 'secondary' : 'primary'}
              onClick={handleButtonStartQuizClick}
              disabled={!isQuizSelected}
              style={{
                ...(isQuizSelected && { backgroundColor: isStartQuiz ? 'red' : 'green', color: 'white' }),
                width: '100%',
              }}
            >
              {isStartQuiz ? 'Stop Quiz' : 'Start Quiz'}
            </Button>
          </Grid>
        </Grid>
        <Grid item xs={4}>
          {(isStartQuiz || ((limitTimeInSecond >= count) && (count > 0))) && (<Typography variant="h6">
            Core: <strong style={{ fontSize: '30px' }}>{core}</strong> ;  Time (seconds): <strong style={{ fontSize: '30px' }}>{count}</strong> / {limitTimeInSecond}
          </Typography>)}
          {questionsResult.map((questions) => (
            <QuestionComponent
              key={questions.id}
              questionId={questions.id}
              questionData={questions.content}
              onSubmitAnswer={handleSubmitAnswer}
              isStartQuiz={isStartQuiz}
            />
          ))}
        </Grid>
        <Grid item xs={2}>
        </Grid>
        <Grid item xs={6}>
          <Typography variant="h6">
            <strong style={{ fontSize: '30px' }}>Rankings</strong>
          </Typography>
          <div>
            {(messages && messages[messages.length - 1]) && (<RankList json={messages[messages.length - 1]} currentUserName={currentUserName} />)}
          </div>
        </Grid>
      </Grid>
    </>
  );
};

export default Home;
