"use client";

import Button from "@/components/Button";
import DateInput from "@/components/DateInput";
import SelectInput, { SelectOption } from "@/components/SelectInput";

import { Form } from "@/components/Form";
import { useApiMutation, useApiQuery } from "@/hooks/query";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "react-toastify";

interface CreateCallRequest {
  callingUserId: string;
  dateCallStarted: string;
}

interface CreateCallResponse {
  id: string;
}

interface ListUsersResponse {
  items: SelectOption[];
}

const CreateCallPage = () => {
  const router = useRouter();

  const [callingUserId, setCallingUserId] = useState("");
  const [dateCallStarted, setDateCallStarted] = useState("");

  const { data: usersList, isLoading: isLoadingUsersList } =
    useApiQuery<ListUsersResponse>("/User/list");

  const { mutateAsync, isIdle } = useApiMutation<
    CreateCallResponse,
    CreateCallRequest
  >("/Call", "post", {
    onSuccess() {
      toast.success("Call created successfully");
      router.replace("/calls");
    },
    onError(error) {
      toast.error(error.join(","));
    },
  });

  const onSubmit = () => {
    if (isIdle) {
      mutateAsync({ callingUserId, dateCallStarted });
    }
  };

  return (
    <Form>
      <Form.Header
        title="New Call"
        description="Made a call? Record it below to keep track of your interactions."
      />
      <Form.Section>
        <SelectInput
          id="callingUserId"
          label="User"
          type="text"
          name="callingUserId"
          autoComplete="off"
          value={callingUserId}
          onChange={(e) => setCallingUserId(e.target.value)}
          options={usersList?.items}
          isLoading={!usersList?.items?.length || isLoadingUsersList}
        />
        <DateInput
          id="dateCallStarted"
          label="Date & Time Call Started"
          name="dateCallStarted"
          autoComplete="off"
          value={dateCallStarted}
          onChange={(e) => setDateCallStarted(e.target.value)}
        />
      </Form.Section>
      <Form.Section>
        <Button
          onClick={onSubmit}
          type="button"
          className={isIdle === false ? "cursor-not-allowed opacity-50" : ""}
        >
          Create Call
        </Button>
      </Form.Section>
    </Form>
  );
};

export default CreateCallPage;
