"use client";

import Button from "@/components/Button";
import TextInput from "@/components/TextInput";
import { Form } from "@/components/Form";
import { useApiMutation } from "@/hooks/query";
import { useRouter } from "next/navigation";
import { useState } from "react";
import { toast } from "react-toastify";

interface CreateUserRequest {
  username: string;
}

interface CreateUserResponse {
  id: string;
}

const CreateUserPage = () => {
  const router = useRouter();

  const [username, setUsername] = useState("");

  const { mutateAsync, isIdle } = useApiMutation<
    CreateUserResponse,
    CreateUserRequest
  >("/User", "post", {
    onSuccess() {
      toast.success("User created successfully");
      router.replace("/users");
    },
    onError(error) {
      toast.error(error.join(","));
    },
  });

  const onSubmit = () => {
    if (isIdle) {
      mutateAsync({ username });
    }
  };

  return (
    <Form>
      <Form.Header
        title="User Profile"
        description="This information will be displayed publicly so be careful what you share."
      />
      <Form.Section>
        <TextInput
          id="username"
          label="Username"
          placeholder="Bobby Tables"
          type="text"
          name="username"
          autoComplete="off"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </Form.Section>
      <Form.Section>
        <Button
          onClick={onSubmit}
          type="button"
          className={isIdle === false ? "cursor-not-allowed opacity-50" : ""}
        >
          Create User
        </Button>
      </Form.Section>
    </Form>
  );
};

export default CreateUserPage;
