import React, { useState } from 'react';
import { Grid, Radio, RadioGroup, FormControlLabel, FormControl, FormLabel, Button } from '@mui/material';
import { useI18nContext } from '@ShareCores';

// Các interface
export interface QuestionDetail {
  Items: QuestionItem[];
  Question: string;
}

export interface QuestionItem {
  Index: string;
  Content: string;
}

interface QuestionComponentProps {
  questionId: string;
  questionData: string;
  isStartQuiz : boolean;
  onSubmitAnswer: (questionId: string, answer: string) => void;
}

export const QuestionComponent: React.FC<QuestionComponentProps> = (
  { questionId, questionData, isStartQuiz, onSubmitAnswer }) => {
  const { t } = useI18nContext();
  const questionDetail: QuestionDetail = JSON.parse(questionData);
  const [selectedAnswer, setSelectedAnswer] = useState('');
  const [submitedAnswer, setSubmitedAnswer] = useState(false);

  const handleAnswerChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSelectedAnswer(event.target.value);
  };

  const handleSubmit = () => {
    if ((onSubmitAnswer)&&(selectedAnswer !== '')) {
      setSubmitedAnswer(true);
      onSubmitAnswer(questionId, selectedAnswer);
    }
  };

  return (
    <Grid container spacing={2} paddingTop={2}>
      <Grid item xs={8}>
        <FormControl component="fieldset" disabled = {!isStartQuiz}>
          <FormLabel component="legend">{questionDetail.Question}</FormLabel>
          <RadioGroup value={selectedAnswer} onChange={handleAnswerChange}>
            {questionDetail.Items.map((item) => (
              <FormControlLabel
                key={item.Index}
                value={item.Index}
                control={<Radio />}
                label={`${item.Index}. ${item.Content}`}
              />
            ))}
          </RadioGroup>
        </FormControl>
      </Grid>
      <Grid item xs={4}>
        <Button variant="contained" color="primary" disabled={submitedAnswer || !isStartQuiz || (selectedAnswer == '')} onClick={handleSubmit}>
          {t('submitAnswer')}
        </Button>
      </Grid>
    </Grid>
  );
};

export default QuestionComponent;
