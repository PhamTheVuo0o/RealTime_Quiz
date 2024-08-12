import { useState } from 'react';
import { CoreService } from "@ShareServices";
import { UserStorage } from "@ShareStores";
import { useMutation } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import {
    Question,
    SubmitAnswerData,
    Quiz
  } from "@ShareModels";

export function useGetListQuestionByQuizId() {
    const [ouput, setOuput] = useState<Question[]>([]);
    const { mutate, isSuccess, isError, isPending } = useMutation({
        mutationFn: CoreService.GetListQuestionByQuizId,
        onSuccess: (data) => {
            if (data.isSuccess && data.data && data.data.questions) {
                setOuput(data.data.questions);
            }
            return data;
        },
    });

    return {
        GetListQuestionByQuizId: mutate,
        isSuccess,
        isError,
        isPending,
        ouput
    };
}

export function useSubmitAnswer() {
    const [ouput, setOuput] = useState<SubmitAnswerData | null>(null);
    const { mutate, isSuccess, isError, isPending } = useMutation({
        mutationFn: CoreService.SubmitAnswer,
        onSuccess: (data) => {
            if (data.isSuccess && data.data) {
                setOuput(data.data);
            }
            return data;
        },
    });

    return {
        SubmitAnswer: mutate,
        isSuccess,
        isError,
        isPending,
        ouput
    };
}

export function useGetListQuiz() {
    const [ouput, setOuput] = useState<Quiz[]>([]);
    const { mutate, isSuccess, isError, isPending } = useMutation({
        mutationFn: CoreService.GetListQuiz,
        onSuccess: (data) => {
            if (data.isSuccess && data.data && data.data.quizs) {
                setOuput(data.data.quizs);
            }
            return data;
        },
    });

    return {
        GetListQuiz: mutate,
        isSuccess,
        isError,
        isPending,
        ouput
    };
}
